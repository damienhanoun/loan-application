using Acquisition.WebApplication.Server.Infrastructure.Acquisition;
using Acquisition.WebApplication.Server.Infrastructure.Azure;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration(builder.Environment.IsDevelopment() ? ConfigurationType.DevelopmentConfiguration : ConfigurationType.AzureAppConfiguration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd",
        builder => builder.WithOrigins("https://localhost:4200", "https://agreeable-wave-01c001d03.5.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddClients(builder.Configuration);
builder.AddTelemetry(!builder.Environment.IsDevelopment());

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontEnd");
app.UseConfiguration();
app.UseFastEndpoints();
app.UseHttpsRedirection();

await app.RunAsync();