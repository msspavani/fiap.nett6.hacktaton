using HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();