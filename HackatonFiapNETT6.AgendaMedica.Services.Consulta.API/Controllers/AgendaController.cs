using System.Security.Claims;
using HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Controllers;

public class AgendaController : ControllerBase
{
    private readonly ILogger<AgendaController> _logger;
    private readonly IMediator _mediator;

    public AgendaController(ILogger<AgendaController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    
    [Authorize(Roles = "Medico")]
    [HttpGet("medico")]
    public async Task<IActionResult> ConsultarConsultasAgendadas()
    {
        var medicoId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(medicoId, out var id))
            return Unauthorized();

        var query = new GetConsultasAgendadasQuery { MedicoId = id };
        var consultas = await _mediator.Send(query);
        return Ok(consultas);
    }
}