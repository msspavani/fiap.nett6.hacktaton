namespace HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.Domain.ViewModels;

    public class MedicoCadastroRequest
    {
        public Guid UsuarioId { get; set; }  // Preenchido após login
        public string Nome { get; set; }
        public string Crm { get; set; }
        public string Especialidade { get; set; }
    }
