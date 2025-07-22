using HackatonFiapNETT6.AgendaMedica.Shared.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Messaging.Consultas;

public class AgendarConsultaMessage
{
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
    public StatusConsulta Status { get; set; }
}