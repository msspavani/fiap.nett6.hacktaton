using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Enums;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.API.Tests;

public class UsuarioTests
{
    [Fact]
    public void Deve_Validar_Senha_Correta()
    {
        // Arrange
        string senha = "SenhaForte@123";
        var (hash, salt) = Usuario.CriarHash(senha);
        var usuario = new Usuario(Guid.NewGuid(), "teste", hash, salt, TipoUsuario.Medico);

        // Act
        bool resultado = usuario.ValidarSenha("SenhaForte@123");

        // Assert
        Assert.True(resultado);
    }
    
    [Fact]
    public void Deve_Falhar_Validacao_Com_Senha_Errada()
    {
        // Arrange
        string senha = "SenhaForte@123";
        var (hash, salt) = Usuario.CriarHash(senha);
        var usuario = new Usuario(Guid.NewGuid(), "teste", hash, salt, TipoUsuario.Medico);

        // Act
        bool resultado = usuario.ValidarSenha("SenhaIncorreta");

        // Assert
        Assert.False(resultado);
    }
    
    [Fact]
    public void Deve_Validar_Senha_Armazenada()
    {
        var salt = Convert.FromHexString("bc9ac9a2-3c20-19d4-0b9b-796e7da93462"); 
        var hashBase64 = "1slsHZYzIQOrCO5koUd5reYXuJjxJ/X5hi+aRNGMJhQ=";

        var usuario = new Usuario(Guid.NewGuid(), "CRM123456", hashBase64, salt, TipoUsuario.Medico);

        Assert.True(usuario.ValidarSenha("SenhaForte@123"));
    }
}