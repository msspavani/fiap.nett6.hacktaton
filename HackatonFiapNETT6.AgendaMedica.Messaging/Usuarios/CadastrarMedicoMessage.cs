namespace HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

public class CadastrarMedicoMessage
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Crm { get; set; }
    public string Especialidade { get; set; }
}