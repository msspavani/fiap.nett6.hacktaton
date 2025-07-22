using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Application.Commands;

public class CadastrarMedicoCommandHandler
{
    private readonly ServiceBusSender _sender;
    public CadastrarMedicoCommandHandler(ServiceBusClient client)
    {
        _sender = client.CreateSender("medico-criar");
    }

    public async Task<Unit> Handle(CadastrarMedicoCommand request, CancellationToken cancellationToken)
    {
        var payload = JsonSerializer.Serialize(request);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(payload))
        {
            Subject = "medico.criar"
        };

        await _sender.SendMessageAsync(message, cancellationToken);
        return Unit.Value;
    }
}