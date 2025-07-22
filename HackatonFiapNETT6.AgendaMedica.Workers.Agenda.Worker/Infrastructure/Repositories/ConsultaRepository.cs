using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Agenda.Worker.Repositories;

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
}