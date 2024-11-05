using Acquisition.WebApplication.Server.Infrastructure.Acquisition;
using Acquisition.WebApplication.Server.Infrastructure.Azure;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration(builder.Environment.IsDevelopment() ? ConfigurationType.DevelopmentConfiguration : ConfigurationType.AzureAppConfiguration);

builder.Services.AddCors(options =>
{
    var frontUrl = builder.Configuration["Acquisition:Front:Public:Url"]!;
    var localFrontUrl = Environment.GetEnvironmentVariable("LOCAL_FRONT_URL");

    var frontUrls = string.IsNullOrEmpty(localFrontUrl) == false ? new[] { localFrontUrl } : new[] { frontUrl };

    options.AddPolicy("AllowFrontEnd",
        b => b.WithOrigins(frontUrls)
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