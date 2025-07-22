namespace HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Tipo { get; set; }
    public string Login { get; set; }
    public string SenhaHash { get; set; }
    public string Salt { get; set; }
    public DateTime CriadoEm { get; set; }
}