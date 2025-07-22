using System.Text.Json;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Messaging.Agenda;
using HackatonFiapNETT6.AgendaMedica.Messaging.Consultas;
using HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Consumers;

public class ResponderConsultaConsumer : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IServiceProvider _provider;

    public ResponderConsultaConsumer(ServiceBusClient client, IServiceProvider provider)
    {
        _processor = client.CreateProcessor("consulta-responder", new ServiceBusProcessorOptions());
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
        var dto = JsonSerializer.Deserialize<RespostaConsultaMessage>(args.Message.Body);

        using var scope = _provider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IConsultaRepository>();
        await repo.AtualizarStatusConsultaAsync(dto.ConsultaId, dto.Aceita);
        await args.CompleteMessageAsync(args.Message);
    }

    private Task TrataErro(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}