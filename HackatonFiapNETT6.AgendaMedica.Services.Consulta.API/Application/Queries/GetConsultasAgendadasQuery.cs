using HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Dtos;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Queries;

public class GetConsultasAgendadasQuery : IRequest<IEnumerable<ConsultaAgendadaDto>>
{
    public Guid MedicoId { get; set; }
}