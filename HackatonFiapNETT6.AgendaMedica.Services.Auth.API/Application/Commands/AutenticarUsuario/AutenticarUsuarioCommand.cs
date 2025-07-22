using HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Responses;
using HackatonFiapNETT6.AgendaMedica.Shared.Enums;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Commands.AutenticarUsuario;

public class AutenticarUsuarioCommand : IRequest<TokenResponse>
{
    public string Login { get; set; }
    public string Senha { get; set; }
    public TipoUsuario Tipo { get; set; } // Médico ou Paciente

    public AutenticarUsuarioCommand(string login, string senha, TipoUsuario tipo)
    {
        Login = login;
        Senha = senha;
        Tipo = tipo;
    }
}
