using Gatherly.App.Configurations;
using Gatherly.App.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
        builder.Configuration,
        typeof(IServiceInstaller).Assembly);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
