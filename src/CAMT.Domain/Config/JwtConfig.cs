namespace CAMT.Domain.Config;

public class JwtConfig
{
    public int ExpirationMinutes { get; set; }
    public string Secret { get; set; }
}