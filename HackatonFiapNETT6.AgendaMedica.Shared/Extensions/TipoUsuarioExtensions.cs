using HackatonFiapNETT6.AgendaMedica.Shared.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Shared.Extensions;

public static class TipoUsuarioExtensions
{
    public static TipoUsuario ToTipoUsuario(this string tipo)
    {
        if (Enum.TryParse<TipoUsuario>(tipo, true, out var result))
            return result;

        throw new ArgumentException($"Tipo de usuário inválido: '{tipo}'");
    }
}