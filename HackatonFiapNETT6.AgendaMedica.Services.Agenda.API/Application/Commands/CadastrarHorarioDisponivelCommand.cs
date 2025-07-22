using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Commands;

public class CadastrarHorarioDisponivelCommand : IRequest
{
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
}