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
        var (hash, saltBytes) = Usuario.CriarHash(senha);
        var usuario = new Usuario(Guid.NewGuid(), "teste", hash, saltBytes, TipoUsuario.Medico);

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
        var (hash, saltBytes) = Usuario.CriarHash(senha);
        var usuario = new Usuario(Guid.NewGuid(), "teste", hash, saltBytes, TipoUsuario.Medico);

        // Act
        bool resultado = usuario.ValidarSenha("SenhaIncorreta");

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void Deve_Validar_Senha_Armazenada()
    {
        // Arrange
        var saltBytes = Convert.FromHexString("4fcd8f7a9c1a0ea2e3e2d7a190b71bc3");
        var senha = "123Quatro#$";
        var hashBase64 = Convert.ToBase64String(Usuario.GerarHash(senha, saltBytes));

        var usuario = new Usuario(
            id: Guid.NewGuid(),
            login: "CRM123456",
            senhaHash: hashBase64,
            salt: saltBytes,
            tipo: TipoUsuario.Medico
        );

        // Act
        bool resultado = usuario.ValidarSenha(senha);

        // Assert
        Assert.True(resultado);
    }
}