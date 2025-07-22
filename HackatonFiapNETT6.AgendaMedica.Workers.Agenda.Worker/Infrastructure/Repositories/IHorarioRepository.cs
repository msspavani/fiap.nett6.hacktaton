namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

public interface IHorarioRepository
{
    Task CadastrarHorarioAsync(Guid medicoId, DateTime dataHora);
    Task<bool> EstaDisponivelAsync(Guid medicoId, DateTime dataHora);
    Task ReservarAsync(Guid medicoId, DateTime dataHora);
}