namespace HackatonFiapNETT6.AgendaMedica.Services.Agenda.API.Application.Dto;

public class AgendarConsultaRequest
{
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
}