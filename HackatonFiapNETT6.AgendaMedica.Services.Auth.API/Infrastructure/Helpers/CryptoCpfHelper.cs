using System.Security.Cryptography;
using System.Text;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Helpers;

public static class CryptoCpfHelper
{
    private static readonly byte[] Chave = Convert.FromBase64String(
        Environment.GetEnvironmentVariable("CRYPTO_KEY") ?? throw new InvalidOperationException("CRYPTO_KEY is not set"));

    private static readonly byte[] Iv = Convert.FromBase64String(
        Environment.GetEnvironmentVariable("CRYPTO_IV") ?? throw new InvalidOperationException("CRYPTO_IV is not set"));

    public static string Criptografar(string cpf)
    {
        using var aes = Aes.Create();
        aes.Key = Chave;
        aes.IV = Iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var plainBytes = Encoding.UTF8.GetBytes(cpf);

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        cs.Write(plainBytes, 0, plainBytes.Length);
        cs.FlushFinalBlock();

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Descriptografar(string criptografado)
    {
        using var aes = Aes.Create();
        aes.Key = Chave;
        aes.IV = Iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var cipherBytes = Convert.FromBase64String(criptografado);

        using var ms = new MemoryStream(cipherBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}