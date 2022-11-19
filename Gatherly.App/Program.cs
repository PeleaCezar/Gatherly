using Gatherly.Persistence;
using Microsoft.EntityFrameworkCore;
using Gatherly.Persistence.Interceptors;
using MediatR;
using Quartz;
using Gatherly.Infrastructure.BackgroundJobs;
using Gatherly.Application.Behaviors;
using FluentValidation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
       .Scan(selector => selector
       .FromAssemblies(
           Gatherly.Infrastructure.AssemblyReference.Assembly,
           Gatherly.Persistence.AssemblyReference.Assembly)
           .AddClasses(false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());


builder.Services.AddMediatR(Gatherly.Application.AssemblyReference.Assembly);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(Gatherly.Application.AssemblyReference.Assembly, includeInternalTypes: true);

string connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>(
    (sp, optionsBuilder) =>
    {
        var inteceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

        optionsBuilder.UseSqlServer(connectionString)
            .AddInterceptors(inteceptor);
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

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
