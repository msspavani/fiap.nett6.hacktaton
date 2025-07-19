using HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Commands.AutenticarUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AutenticarUsuarioCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(token);

        }
        catch (Exception e)
        {
            _logger.LogError("Erro ao logar usuario {usuario}", command.Login);
            return NoContent();
        }

    }
}