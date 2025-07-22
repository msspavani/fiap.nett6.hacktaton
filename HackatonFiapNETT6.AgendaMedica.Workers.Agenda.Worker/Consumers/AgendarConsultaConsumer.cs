using System.Text.Json;
using Azure.Messaging.ServiceBus;
using HackatonFiapNETT6.AgendaMedica.Messaging.Consultas;
using HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Consumers;

public class AgendarConsultaConsumer : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IServiceProvider _provider;
    private readonly ILogger<AgendarConsultaConsumer> _logger;

    public AgendarConsultaConsumer(ServiceBusClient client, IServiceProvider provider, ILogger<AgendarConsultaConsumer> logger)
    {
        _processor = client.CreateProcessor("agendar-consulta", new ServiceBusProcessorOptions());
        _provider = provider;
        _logger = logger;
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
        var horarioRepository = scope.ServiceProvider.GetRequiredService<IHorarioRepository>();

        if (!await horarioRepository.EstaDisponivelAsync(dto.MedicoId, dto.DataHora))
        {
            _logger.LogInformation("Impossivel registrar horario duplicado para {medico}. horario {Horario}",
                dto.MedicoId, dto.DataHora);
            return;
        }
        
        
        await horarioRepository.ReservarAsync(dto.MedicoId, dto.DataHora);
        await repo.InserirConsultaAsync(dto.MedicoId, dto.PacienteId, dto.DataHora);
        await args.CompleteMessageAsync(args.Message);
    }

    private Task TrataErro(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}