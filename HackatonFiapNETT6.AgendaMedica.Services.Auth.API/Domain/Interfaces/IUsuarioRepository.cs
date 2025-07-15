using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorLoginAsync(string login, TipoUsuario tipo);
}