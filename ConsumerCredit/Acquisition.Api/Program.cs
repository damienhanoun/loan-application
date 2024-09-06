using System.Reflection;
using Acquisition.Api.Persistence.Database;
using Acquisition.Api.Scaffolding;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Transient; });
builder.Services.AddDbContext<AcquisitionContext>(
    options => options.UseNpgsql("Host=localhost;Port=5432;Database=acquisition;User ID=postgres;Password=password;"),
    ServiceLifetime.Transient);
builder.Services.AddServicesAndRepositories(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFastEndpoints();
app.UseHttpsRedirection();

await app.RunAsync();