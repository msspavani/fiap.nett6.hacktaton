using System.Data;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers;
using Microsoft.Data.SqlClient;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton(sp =>
{
    var conn = builder.Configuration.GetConnectionString("AzureServiceBus");
    return new ServiceBusClient(conn);
});
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<CriarUsuarioConsumer>();

var host = builder.Build();
host.Run();