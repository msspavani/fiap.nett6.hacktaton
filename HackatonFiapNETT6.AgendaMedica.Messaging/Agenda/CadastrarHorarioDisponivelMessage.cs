namespace HackatonFiapNETT6.AgendaMedica.Messaging.Agenda;

public class CadastrarHorarioDisponivelMessage
{
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
}