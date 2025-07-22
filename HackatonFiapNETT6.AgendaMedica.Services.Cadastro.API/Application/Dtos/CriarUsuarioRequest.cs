namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Domain.ViewModels;

public class CriarUsuarioRequest
{
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Tipo { get; set; } 
}