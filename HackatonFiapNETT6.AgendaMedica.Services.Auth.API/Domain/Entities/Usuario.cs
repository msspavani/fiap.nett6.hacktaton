using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Enums;
using Konscious.Security.Cryptography;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;

public class Usuario
{
    [Key]
    public Guid Id { get; private set; }
    public string Login { get; private set; }
    public string SenhaHash { get; private set; }
    public TipoUsuario Tipo { get; private set; }
    
    
    [Column(TypeName = "varbinary(16)")] 
    public byte[] Salt { get; private set; } 

    public Usuario(Guid id, string login, string senhaHash, byte[] salt, TipoUsuario tipo)
    {
        Id = id;
        Login = login;
        SenhaHash = senhaHash;
        Salt = salt;
        Tipo = tipo;
    }
    public bool ValidarSenha(string senhaTextoClaro)
    {
        var hashGerado = GerarHash(senhaTextoClaro, Salt);

        return hashGerado.SequenceEqual(Convert.FromBase64String(SenhaHash));
    }

    public static byte[] GerarHash(string senha, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha))
        {
            Salt = salt,
            DegreeOfParallelism = 4, 
            MemorySize = 65536, 
            Iterations = 4
        };

        return argon2.GetBytes(32); 
    }

    public static (string hashBase64, byte[] salt) CriarHash(string senha)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = GerarHash(senha, salt);
        return (Convert.ToBase64String(hash), salt);
    }
}