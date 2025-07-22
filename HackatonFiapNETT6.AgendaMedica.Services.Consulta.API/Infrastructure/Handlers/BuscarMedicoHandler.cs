using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Queries;
using HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Repositories.Handlers;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Infrastructure.Handlers;

public class BuscarMedicosHandler
{
    private readonly IMedicoRepository _repo;

    public BuscarMedicosHandler(IMedicoRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<MedicoDisponivelDto>> Handle(BuscarMedicosQuery request, CancellationToken cancellationToken)
    {
        return _repo.BuscarPorEspecialidadeAsync(request.Especialidade);
    }
}