using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Repositories.Handlers;

public interface IMedicoRepository
{
    Task<IEnumerable<MedicoDisponivelDto>> BuscarPorEspecialidadeAsync(string especialidade);
}