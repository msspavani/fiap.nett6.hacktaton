using System.Security.Claims;
using HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Application.Commands;
using HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Controllers.V1;

[ApiController]
[Route("api/cadastro")]
public class CadastroController : ControllerBase
{
    private readonly ILogger<CadastroController> _logger;
    private readonly IMediator _mediator;

    public CadastroController(ILogger<CadastroController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [Authorize(Roles = "Medico")]
    [HttpPost("medico")]
    public async Task<IActionResult> CadastrarMedico([FromBody] MedicoCadastroRequest request)
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(usuarioId, out var id))
            return Unauthorized();

        var command = new CadastrarMedicoCommand
        {
            UsuarioId = id,
            Nome = request.Nome,
            Crm = request.Crm,
            Especialidade = request.Especialidade
        };

        await _mediator.Send(command);
        return Accepted();
    }
}