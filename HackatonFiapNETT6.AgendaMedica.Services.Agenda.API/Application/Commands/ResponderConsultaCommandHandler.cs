using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;

public class ResponderConsultaCommandHandler
{
    private readonly ServiceBusSender _sender;
    private readonly ILogger<ResponderConsultaCommandHandler> _logger;

    public ResponderConsultaCommandHandler(ServiceBusClient client, ILogger<ResponderConsultaCommandHandler> logger)
    {
        _sender = client.CreateSender("consulta-responder");
        _logger = logger;
    }

    public async Task Handle(ResponderConsultaCommand command, CancellationToken cancellationToken)
    {
        var payload = JsonSerializer.Serialize(new
        {
            command.ConsultaId,
            command.MedicoId,
            command.Aceita
        });

        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(payload))
        {
            Subject = "consulta.resposta",
            ContentType = "application/json"
        };

        await _sender.SendMessageAsync(message, cancellationToken);
        _logger.LogInformation("Resposta da consulta enviada para fila.");
        
    }
}