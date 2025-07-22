namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Dtos;

public class ConsultaAgendadaDto
{
    public Guid ConsultaId { get; set; }
    public Guid PacienteId { get; set; }
    public DateTime DataHora { get; set; }
    public string Status { get; set; } = string.Empty;
}