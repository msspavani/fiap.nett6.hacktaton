using System.Data;
using Dapper;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;

public class MedicoRepository : IMedicoRepository
{
    
    private readonly IDbConnection _connection;

    public MedicoRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task InserirAsync(Guid usuarioId, string nome, string crm, string especialidade)
    {
        const string sql = @"
        INSERT INTO Medicos (Id, Nome, Crm, Especialidade)
        VALUES (@Id, @Nome, @Crm, @Especialidade)";
    
        await _connection.ExecuteAsync(sql, new
        {
            Id = usuarioId,
            Nome = nome,
            Crm = crm,
            Especialidade = especialidade
        });
    }
    
}