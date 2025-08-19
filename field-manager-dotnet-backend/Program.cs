using FieldManagerDotnetBackend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<DemoDataService>();

// Add controllers support
builder.Services.AddControllers();

// Build application
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();