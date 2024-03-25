using Acquisition.Api.Scaffolding;
using Acquisition.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Transient; });
builder.Services.AddDbContext<AcquisitionContext>(
    options => options.UseNpgsql("Host=localhost;Port=5432;Database=acquisition;User ID=postgres;Password=password;"), ServiceLifetime.Transient);
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();