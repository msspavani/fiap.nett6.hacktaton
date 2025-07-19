using System.Data;
using System.Reflection;
using System.Text.Json.Serialization;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Repositories;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Security;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});


builder.Services.AddControllers() .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();

app.UseRouting();
app.UseHttpMetrics(); 
app.MapMetrics();   
app.MapControllers();

app.Run("http://0.0.0.0:80");
app.Run();

