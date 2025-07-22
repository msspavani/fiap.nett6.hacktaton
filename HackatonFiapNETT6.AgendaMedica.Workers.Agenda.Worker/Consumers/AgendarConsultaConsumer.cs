using System.Text.Json;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Messaging.Consultas;
using HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Repositories;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Consumers;

public class AgendarConsultaConsumer : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IServiceProvider _provider;

    public AgendarConsultaConsumer(ServiceBusClient client, IServiceProvider provider)
    {
        _processor = client.CreateProcessor("agendar-consulta", new ServiceBusProcessorOptions());
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
        var json = args.Message.Body.ToString();
        var dto = JsonSerializer.Deserialize<AgendarConsultaMessage>(json);

        using var scope = _provider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IConsultaRepository>();

        await repo.InserirConsultaAsync(dto.MedicoId, dto.PacienteId, dto.DataHora);
        await args.CompleteMessageAsync(args.Message);
    }

    private Task TrataErro(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}