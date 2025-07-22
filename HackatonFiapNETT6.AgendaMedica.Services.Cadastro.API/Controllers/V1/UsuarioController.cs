using HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Application.Commands;
using HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Domain.ViewModels;
using HackatonFiapNETT6.AgendaMedica.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Controllers.V1;

public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(IMediator mediator, ILogger<UsuarioController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand request)
    {
        await _mediator.Send(request);
        return Accepted();
    }

    
}