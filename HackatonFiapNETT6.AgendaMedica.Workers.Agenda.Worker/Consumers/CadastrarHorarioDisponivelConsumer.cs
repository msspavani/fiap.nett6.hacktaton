using System.Text.Json;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Messaging.Agenda;
using HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Consumers;

public class CadastrarHorarioDisponivelConsumer : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IServiceProvider _provider;

    public CadastrarHorarioDisponivelConsumer(ServiceBusClient client, IServiceProvider provider)
    {
        _processor = client.CreateProcessor("horario-disponivel-criar", new ServiceBusProcessorOptions());
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
        var dto = JsonSerializer.Deserialize<CadastrarHorarioDisponivelMessage>(args.Message.Body);

        using var scope = _provider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IHorarioRepository>();
        await repo.CadastrarHorarioAsync(dto.MedicoId, dto.DataHora);
        await args.CompleteMessageAsync(args.Message);
    }

    private Task TrataErro(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}