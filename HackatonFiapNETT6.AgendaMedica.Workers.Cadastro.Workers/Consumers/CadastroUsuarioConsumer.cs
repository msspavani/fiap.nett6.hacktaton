using System.Text.Json;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;
using Microsoft.Data.SqlClient;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers;
public class CriarUsuarioConsumer : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IConfiguration _configuration;
    private readonly IUsuarioWriteRepository _repository;

    public CriarUsuarioConsumer(ServiceBusClient client, IConfiguration configuration, IUsuarioWriteRepository repository)
    {
        _processor = client.CreateProcessor("usuario-criar", new ServiceBusProcessorOptions());
        _configuration = configuration;
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += ProcessarMensagem;
        _processor.ProcessErrorAsync += TrataErro;

        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task ProcessarMensagem(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        var usuario = JsonSerializer.Deserialize<CriarUsuarioMessage>(body);

        if (usuario is not null)
            await _repository.InserirAsync(new Usuario(usuario.UsuarioId,
                    usuario.LoginCriptografado,
                    usuario.SenhaHash,
                    usuario.Salt,
                    usuario.Tipo)
                , CancellationToken.None);

        await args.CompleteMessageAsync(args.Message);
    }

    private Task TrataErro(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}