using System.Reflection;
using Acquisition.Api.Infrastructure.Azure;
using Acquisition.Api.Infrastructure.Persistence.Database;
using Acquisition.Api.Scaffolding;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

// The initialized WebApplicationBuilder provides default configuration and calls AddUserSecrets when the EnvironmentName is Development
var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration(builder.Environment.IsDevelopment() ? ConfigurationType.DevelopmentConfiguration : ConfigurationType.AzureAppConfiguration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddFastEndpoints();
builder.Services.AddOpenApiDocument();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Transient; });
builder.Services.AddAcquisitionDatabase(builder.Configuration);
builder.Services.AddServicesAndRepositories(Assembly.GetExecutingAssembly());
builder.AddTelemetry(!builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<AcquisitionContext>();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any()) await dbContext.Database.MigrateAsync();
    }

    app.UseOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseConfiguration();

app.UseFastEndpoints();
app.UseHttpsRedirection();

await app.RunAsync();