using System.Data;
using System.Reflection;
using System.Text.Json.Serialization;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Repositories;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Security;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositório Dapper
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// JWT Generator
builder.Services.AddScoped<JwtTokenGenerator>();

// MediatR
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
app.MapControllers();
app.Run();

