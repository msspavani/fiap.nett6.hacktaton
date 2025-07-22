using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;

public interface IMedicoRepository
{
    Task InserirAsync(Guid usuarioId, string nome, string crm, string especialidade);
}