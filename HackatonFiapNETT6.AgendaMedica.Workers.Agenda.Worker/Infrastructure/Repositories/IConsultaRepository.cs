namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Repositories;

public interface IConsultaRepository
{
    Task InserirConsultaAsync(Guid medicoId, Guid pacienteId, DateTime dataHora);
}