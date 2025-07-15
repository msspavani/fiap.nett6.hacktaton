using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Events;

public class UsuarioAutenticadoEvent
{
    public Guid UsuarioId { get; }
    public TipoUsuario Tipo { get; }

    public UsuarioAutenticadoEvent(Guid usuarioId, TipoUsuario tipo)
    {
        UsuarioId = usuarioId;
        Tipo = tipo;
    }
}