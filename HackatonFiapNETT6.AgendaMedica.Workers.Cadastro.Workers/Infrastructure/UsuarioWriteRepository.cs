using System.Data;
using Dapper;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;

public class UsuarioWriteRepository
{
    private readonly IDbConnection _connection;

    public UsuarioWriteRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task InserirAsync(UsuarioDto usuario, CancellationToken cancellationToken)
    {
        const string sql = """
                               INSERT INTO Usuarios
                               (Id, Nome, Cpf, Email, Tipo, Login, SenhaHash, Salt, CriadoEm)
                               VALUES
                               (@Id, @Nome, @Cpf, @Email, @Tipo, @Login, @SenhaHash, @Salt, @CriadoEm)
                           """;

        await _connection.ExecuteAsync(sql, new
        {
            usuario.Id,
            usuario.Nome,
            usuario.Cpf,
            usuario.Email,
            usuario.Tipo,
            usuario.Login,
            usuario.SenhaHash,
            usuario.Salt,
            usuario.CriadoEm
        });
    }
}