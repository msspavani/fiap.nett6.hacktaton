namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

public interface IConsultaRepository
{
    Task InserirConsultaAsync(Guid medicoId, Guid pacienteId, DateTime dataHora);
    
    Task AtualizarStatusConsultaAsync(Guid consultaId, bool aceita);
}