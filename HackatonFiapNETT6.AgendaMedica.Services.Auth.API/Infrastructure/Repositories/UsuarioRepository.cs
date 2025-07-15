using System.Data;
using Dapper;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Enums;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _connection;

    public UsuarioRepository(IConfiguration configuration)
    {
        _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<Usuario?> ObterPorLoginAsync(string login, TipoUsuario tipo)
    {
        string query = "SELECT * FROM Usuarios WHERE Login = @login AND Tipo = @tipo";

        return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { login, tipo });
    }
}