using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;

public class AgendarConsultaCommandHandler
{
    private readonly ServiceBusSender _sender;

    public AgendarConsultaCommandHandler(ServiceBusClient client)
    {
        _sender = client.CreateSender("agendar-consulta");
    }

    public async Task Handle(AgendarConsultaCommand command, CancellationToken cancellationToken)
    {
        var evento = new
        {
            PacienteId = command.PacienteId,
            MedicoId = command.MedicoId,
            DataHora = command.DataHora
        };

        var json = JsonSerializer.Serialize(evento);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(json))
        {
            ContentType = "application/json",
            Subject = "consulta.agendar"
        };

        await _sender.SendMessageAsync(message, cancellationToken);
        
    }
}