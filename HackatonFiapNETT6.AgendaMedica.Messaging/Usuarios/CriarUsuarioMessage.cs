using HackatonFiapNETT6.AgendaMedica.Shared.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

public class CriarUsuarioMessage
{
    public Guid UsuarioId { get; set; }
    public string LoginCriptografado { get; set; }
    public string SenhaHash { get; set; }
    public byte[] Salt { get; set; }
    public TipoUsuario Tipo { get; set; }

    public string Login { get; set; }
}