using System.Text;
using Azure.Messaging.ServiceBus;
using MediatR;
using System.Text.Json;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Shared.Extensions;


namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Application.Commands;

public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand>
{
    private readonly ServiceBusSender _sender;
    private readonly ILogger<CriarUsuarioCommandHandler> _logger;

    public CriarUsuarioCommandHandler(ServiceBusClient client, ILogger<CriarUsuarioCommandHandler> logger)
    {
        _logger = logger;
        _sender = client.CreateSender("usuario-criar");
    }

    public async Task Handle(CriarUsuarioCommand command, CancellationToken cancellationToken)
    {
        var (senhaHash, saltBytes) = Usuario.CriarHash(command.Senha);

        var usuario = new Usuario(
            Guid.NewGuid(),
            command.Login,
            senhaHash,
            saltBytes,
            command.Tipo.ToTipoUsuario()        );

        var evento = new CriarUsuarioMessage()
        {
            UsuarioId = usuario.Id,
            Login = usuario.Login,
            SenhaHash = usuario.SenhaHash,
            Salt = usuario.Salt,
            Tipo = usuario.Tipo
        };

        var body = JsonSerializer.Serialize(evento);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
        {
            ContentType = "application/json",
            Subject = "usuario.criar"
        };
        await _sender.SendMessageAsync(message, cancellationToken);

        
    }
}