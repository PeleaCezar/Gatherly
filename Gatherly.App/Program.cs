using Gatherly.Persistence;
using Microsoft.EntityFrameworkCore;
using Gatherly.Persistence.Interceptors;
using MediatR;
using Quartz;
using Gatherly.Infrastructure.BackgroundJobs;
using Gatherly.Application.Behaviors;
using FluentValidation;
using Gatherly.Infrastructure.Idempotence;
using Gatherly.App.Middlewares;
using Scrutor;
using Gatherly.Domain.Repositories;
using Gatherly.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//when someone injects IMemberRepository from a Constructor, they are going to get an instance of CachedMemberREpository
//if you recall CachedMemberRepository is injecting memberRepository inside of its constructor
builder.Services.AddScoped<MemberRepository>();
builder.Services.AddScoped<IMemberRepository, CachedMemberRepository>();


// second aproach
//builder.Services.AddScoped<IMemberRepository>(provider =>
//{
//    var memberRepository = provider.GetService<MemberRepository>();

//    return new CachedMemberRepository(
//        memberRepository,
//        provider.GetService<IMemoryCache>()!);
//});

//third aproach with Scrutor
//builder.Services.AddScoped<IMemberRepository, MemberRepository>();
//builder.Services.Decorate<IMemberRepository, CachedMemberRepository>();

builder.Services
       .Scan(selector => selector
       .FromAssemblies(
           Gatherly.Infrastructure.AssemblyReference.Assembly,
           Gatherly.Persistence.AssemblyReference.Assembly)
           .AddClasses(false)
           .UsingRegistrationStrategy(RegistrationStrategy.Skip)
           .AsImplementedInterfaces()
           .WithScopedLifetime());


builder.Services.AddMediatR(Gatherly.Application.AssemblyReference.Assembly);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(Gatherly.Application.AssemblyReference.Assembly, includeInternalTypes: true);

builder.Services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

string connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

builder.Services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>(
    //(sp, optionsBuilder) =>
    //{
    //    var outboxInterceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!;
    //    var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;

    //    optionsBuilder.UseSqlServer(connectionString)
    //        .AddInterceptors(
    //            outboxInterceptor,
    //            auditableInterceptor);
    //});
    (sp, optionsBuilder) =>
    {
        optionsBuilder.UseSqlServer(connectionString);
    });

builder.Services.AddQuartz(configure =>
{
    var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

    configure
        .AddJob<ProcessOutboxMessagesJob>(jobKey)
        .AddTrigger(
            trigger =>
                trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule =>
                            schedule.WithIntervalInSeconds(10)
                                .RepeatForever()));

    configure.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService();

builder
    .Services
    .AddControllers()
    .AddApplicationPart(Gatherly.Presentation.AssemblyReference.Assembly);


builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
