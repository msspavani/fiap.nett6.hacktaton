using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Queries;

public class BuscarMedicosQuery :  IRequest<IEnumerable<MedicoDisponivelDto>>
{
    public string Especialidade { get; set; }
}