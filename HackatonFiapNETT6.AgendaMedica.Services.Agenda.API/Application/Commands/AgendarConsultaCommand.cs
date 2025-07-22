using HackatonFiapNETT6.AgendaMedica.Shared.Enums;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;

public class AgendarConsultaCommand : IRequest
{
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
    
    public StatusConsulta Status { get; set; }
}