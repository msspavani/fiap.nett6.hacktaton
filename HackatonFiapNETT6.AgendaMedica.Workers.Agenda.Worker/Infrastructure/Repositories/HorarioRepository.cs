using System.Data;
using Dapper;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

public class HorarioRepository : IHorarioRepository
{
    private readonly IDbConnection _connection;

    public HorarioRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task CadastrarHorarioAsync(Guid medicoId, DateTime dataHora)
    {
        const string sql = @"
        INSERT INTO HorariosDisponiveis (Id, MedicoId, DataHora, Reservado)
        VALUES (@Id, @MedicoId, @DataHora, 0)
        ON CONFLICT (MedicoId, DataHora) DO NOTHING";

        await _connection.ExecuteAsync(sql, new
        {
            Id = Guid.NewGuid(),
            MedicoId = medicoId,
            DataHora = dataHora
        });
    }

    public async Task<bool> EstaDisponivelAsync(Guid medicoId, DateTime dataHora)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM HorariosDisponiveis
        WHERE MedicoId = @MedicoId AND DataHora = @DataHora AND Reservado = 0";

        return await _connection.ExecuteScalarAsync<bool>(sql, new { medicoId, dataHora });
    }

    public async Task ReservarAsync(Guid medicoId, DateTime dataHora)
    {
        const string sql = @"
        UPDATE HorariosDisponiveis
        SET Reservado = 1
        WHERE MedicoId = @MedicoId AND DataHora = @DataHora";

        await _connection.ExecuteAsync(sql, new { medicoId, dataHora });
    }

}