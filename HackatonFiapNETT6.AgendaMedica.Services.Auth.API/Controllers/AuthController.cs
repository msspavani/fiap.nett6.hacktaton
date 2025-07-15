using HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Commands.AutenticarUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AutenticarUsuarioCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(token);
    }
}