namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Application.Commands;

public class CadastrarMedicoCommand
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Crm { get; set; }
    public string Especialidade { get; set; }
}