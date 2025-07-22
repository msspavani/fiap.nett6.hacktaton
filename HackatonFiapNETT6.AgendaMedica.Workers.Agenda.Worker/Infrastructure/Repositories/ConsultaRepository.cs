using System.Data;
using Dapper;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Infrastructure.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    private readonly IDbConnection _connection;

    public ConsultaRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task InserirConsultaAsync(Guid medicoId, Guid pacienteId, DateTime dataHora)
    {
        const string sql = @"
            INSERT INTO Consultas (Id, MedicoId, PacienteId, DataHora, Status)
            VALUES (@Id, @MedicoId, @PacienteId, @DataHora, 'AGENDADA')";
        
        await _connection.ExecuteAsync(sql, new
        {
            Id = Guid.NewGuid(),
            MedicoId = medicoId,
            PacienteId = pacienteId,
            DataHora = dataHora
        });
    }
    
    public async Task AtualizarStatusConsultaAsync(Guid consultaId, bool aceita)
    {
        const string sql = @"
        UPDATE Consultas
        SET Status = @Status
        WHERE Id = @Id";

        await _connection.ExecuteAsync(sql, new
        {
            Id = consultaId,
            Status = aceita ? "CONFIRMADA" : "RECUSADA"
        });
    }

}