namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Responses;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpiraEm { get; set; }

    public TokenResponse(string token, DateTime expiraEm)
    {
        Token = token;
        ExpiraEm = expiraEm;
    }
}