namespace HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

public class MedicoDisponivelDto
{
    public Guid MedicoId { get; set; }
    public string Nome { get; set; }
    public string Especialidade { get; set; }
    public string Crm { get; set; }
}