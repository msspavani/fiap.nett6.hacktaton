using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace HackatonFiapNETT6.AgendaMedica.Shared.Security;


public static class PasswordHasher
{
    public static (string hashBase64, byte[] saltBytes) CriarHash(string senha)
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        var hash = GerarHash(senha, salt);
        return (Convert.ToBase64String(hash), salt);
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

    public static bool ValidarSenha(string senhaTextoClaro, string hashBase64, byte[] salt)
    {
        var hashGerado = GerarHash(senhaTextoClaro, salt);
        return hashGerado.SequenceEqual(Convert.FromBase64String(hashBase64));
    }
}