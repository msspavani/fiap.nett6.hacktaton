using System.Data;
using Dapper;
using HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Dtos;
using HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Application.Queries;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Repositories.Handlers;

public class GetConsultasAgendadasHandler
{
    private readonly IDbConnection _connection;

    public GetConsultasAgendadasHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<ConsultaAgendadaDto>> Handle(GetConsultasAgendadasQuery query, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id AS ConsultaId, PacienteId, DataHora, Status
            FROM Consultas
            WHERE MedicoId = @MedicoId AND Status = 'AGENDADA'
            ORDER BY DataHora";

        return await _connection.QueryAsync<ConsultaAgendadaDto>(sql, new { query.MedicoId });
    }
}