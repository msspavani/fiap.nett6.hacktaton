using HackatonFiapNETT6.AgendaMedica.Shared.Enums;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Application.Commands;

public class CriarUsuarioCommand : IRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Tipo { get; set; } 
}
