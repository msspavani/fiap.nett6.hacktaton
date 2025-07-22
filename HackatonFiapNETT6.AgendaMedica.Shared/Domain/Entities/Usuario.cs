using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using HackatonFiapNETT6.AgendaMedica.Shared.Enums;
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

    public Usuario() { }
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
      
        Console.WriteLine($"Senha em bytes: {BitConverter.ToString(Encoding.UTF8.GetBytes(senhaTextoClaro))}");
        
        
        var hashGerado = GerarHash(senhaTextoClaro, Salt);
        Console.WriteLine($"Senha em bytes: {BitConverter.ToString(hashGerado)}");
        
        // var senha = "123Quatro$";
        // var saltBytes = new byte[] { 123, 239, 63, 11, 214, 214, 37, 77, 196, 77, 179, 209, 22, 232, 212, 86 };
        // var hashGerado = Usuario.GerarHash(senha, saltBytes);
        // Console.WriteLine($"Hash: {Convert.ToBase64String(hashGerado)}");
        
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

    public static (string hashBase64, byte[] saltBytes) CriarHash(string senha)
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        var hash = GerarHash(senha, salt);
        return (Convert.ToBase64String(hash), salt);
    }
}