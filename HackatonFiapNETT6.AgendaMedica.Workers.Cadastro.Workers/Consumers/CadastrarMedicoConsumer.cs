using System.Text.Json;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers;

public class CadastrarMedicoConsumer : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _provider;

    public CadastrarMedicoConsumer(ServiceBusClient client, IConfiguration configuration, IServiceProvider provider)
    {
        _processor = client.CreateProcessor("medico-criar");
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += ProcessarMensagem;
        _processor.ProcessErrorAsync += TrataErro;

        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task ProcessarMensagem(ProcessMessageEventArgs args)
    {
        var dto = JsonSerializer.Deserialize<CadastrarMedicoMessage>(args.Message.Body);
        using var scope = _provider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IMedicoRepository>();

        await repo.InserirAsync(dto.UsuarioId, dto.Nome, dto.Crm, dto.Especialidade);
        await args.CompleteMessageAsync(args.Message);
    }

    private Task TrataErro(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}