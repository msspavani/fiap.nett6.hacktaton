using System.Security.Claims;
using HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;
using HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Controllers;

[ApiController]
[Route("api/agenda")]
public class AgendaController : ControllerBase
{
    private readonly IMediator _mediator;
    public readonly ILogger<AgendaController> _logger;

    public AgendaController(IMediator mediator, ILogger<AgendaController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Authorize(Roles = "Paciente")]
    [HttpPost("agendamentos")]
    public async Task<IActionResult> AgendarConsulta([FromBody] AgendarConsultaRequest request)
    {
        var pacienteId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        
        _logger.LogInformation("Novo agendamento de consulta para o paciente {paciente}", pacienteId);
        if (!Guid.TryParse(pacienteId, out var id))
            return Unauthorized("Paciente inválido");

        var command = new AgendarConsultaCommand
        {
            PacienteId = id,
            MedicoId = request.MedicoId,
            DataHora = request.DataHora
        };

        await _mediator.Send(command);
        return Accepted();
    }
    
    
    [Authorize(Roles = "Medico")]
    [HttpPost("consultas/{id}/resposta")]
    public async Task<IActionResult> ResponderConsulta(Guid id, [FromBody] RespostaConsultaRequest request)
    {
        var medicoId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(medicoId, out var mId))
            return Unauthorized();

        var command = new ResponderConsultaCommand
        {
            ConsultaId = id,
            MedicoId = mId,
            Aceita = request.Aceita
        };

        await _mediator.Send(command);
        return Accepted();
    }
    
    [Authorize(Roles = "Medico")]
    [HttpPost("disponibilidade")]
    public async Task<IActionResult> CadastrarHorario([FromBody] HorariosDisponiveisRequest request)
    {
        var medicoId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(medicoId, out var mId))
            return Unauthorized();

        var command = new CadastrarHorarioDisponivelCommand
        {
            MedicoId = mId,
            DataHora = request.DataHora
        };

        await _mediator.Send(command);
        return Accepted();
    }
    
}