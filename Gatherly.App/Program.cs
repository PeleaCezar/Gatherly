using Gatherly.App.Configurations;
using Gatherly.App.Middlewares;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
        builder.Configuration,
        typeof(IServiceInstaller).Assembly);

builder.Host.UseSerilog((context, configuration) =>
 configuration
      //.WriteTo.Console()
      //.MinimumLevel.Information());
      .ReadFrom.Configuration(context.Configuration));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
