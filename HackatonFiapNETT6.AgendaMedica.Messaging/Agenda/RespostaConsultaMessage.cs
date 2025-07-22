namespace HackatonFiapNETT6.AgendaMedica.Messaging.Agenda;

public class RespostaConsultaMessage
{
    public Guid ConsultaId { get; set; }
    public Guid MedicoId { get; set; }
    public bool Aceita { get; set; }
}