using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Shared.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;

public interface IUsuarioReadOnlyRepository
{
    Task<Usuario?> ObterPorLoginAsync(string login, TipoUsuario tipo);
}