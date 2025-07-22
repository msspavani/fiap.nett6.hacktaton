using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;

public class ResponderConsultaCommand : IRequest
{
    public Guid ConsultaId { get; set; }
    public Guid MedicoId { get; set; }
    public bool Aceita { get; set; }
}