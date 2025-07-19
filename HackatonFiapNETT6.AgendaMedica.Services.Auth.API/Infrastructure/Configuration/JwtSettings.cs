namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Configuration;

public class JwtSettings
{
      public string Secret { get; set; }
      public string Issuer { get; set; }
      public string Audience { get; set; }
}