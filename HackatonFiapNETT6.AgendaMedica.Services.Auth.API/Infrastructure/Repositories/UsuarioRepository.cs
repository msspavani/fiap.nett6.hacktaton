using System.Data;
using System.Reflection.Metadata.Ecma335;
using Dapper;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Enums;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<UsuarioRepository> _logger;

    public UsuarioRepository(IConfiguration configuration, ILogger<UsuarioRepository> logger)
    {
        _logger = logger;
        _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<Usuario?> ObterPorLoginAsync(string login, TipoUsuario tipo)
    {
        try
        {
            var loginEncrypted = CryptoCpfHelper.Criptografar(login);
            string query = "SELECT * FROM Usuarios WHERE Login = @login AND Tipo = @tipo";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { login = loginEncrypted, tipo });

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, "Erro ao consultar usuário {login}", login);
            return null;
        }

    }
}