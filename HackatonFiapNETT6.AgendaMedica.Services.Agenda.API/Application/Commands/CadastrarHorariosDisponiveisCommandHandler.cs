using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;

public class CadastrarHorariosDisponiveisCommandHandler : IRequestHandler<CadastrarHorarioDisponivelCommand>
{
    private readonly ServiceBusSender _sender;
    private readonly ILogger<CadastrarHorariosDisponiveisCommandHandler> _logger;

    public CadastrarHorariosDisponiveisCommandHandler(ServiceBusClient client, 
                                                      ILogger<CadastrarHorariosDisponiveisCommandHandler> logger)
    {
        _sender = client.CreateSender("horario-disponivel-criar");
        _logger = logger;
    }

    public async Task Handle(CadastrarHorarioDisponivelCommand command, CancellationToken cancellationToken)
    {
        var payload = JsonSerializer.Serialize(command);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(payload))
        {
            Subject = "horario.criar",
            ContentType = "application/json"
        };

        await _sender.SendMessageAsync(message, cancellationToken);
        _logger.LogInformation("Horário disponível publicado: {DataHora}", command.DataHora);
    }
}