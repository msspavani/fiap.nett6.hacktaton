using System.Data;
using Dapper;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

namespace HackatonFiapNETT6.AgendaMedica.Services.Consulta.API.Repositories.Handlers;

public class MedicoRepository
{
    private readonly IDbConnection _connection;

    public MedicoRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<IEnumerable<MedicoDisponivelDto>> BuscarPorEspecialidadeAsync(string especialidade)
    {
        const string sql = @"
        SELECT Id AS MedicoId, Nome, Crm, Especialidade
        FROM Medicos
        WHERE Especialidade LIKE @Especialidade";

        return await _connection.QueryAsync<MedicoDisponivelDto>(sql, new { Especialidade = $"%{especialidade}%" });
    }
}