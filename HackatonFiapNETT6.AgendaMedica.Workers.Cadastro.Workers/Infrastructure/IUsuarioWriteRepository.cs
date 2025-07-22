using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;

public interface IUsuarioWriteRepository
{
    Task InserirAsync(Usuario usuario, CancellationToken cancellationToken);
}
