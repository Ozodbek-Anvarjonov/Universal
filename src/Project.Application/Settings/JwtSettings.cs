namespace Project.Application.Settings;

public class JwtSettings
{
    public bool ValidateIssuer { get; set; }

    public string ValidIssuer { get; set; } = default!;

    public bool ValidateAudience { get; set; }

    public string ValidAudience { get; set; } = default!;

    public bool ValidateLifeTime { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }

    public string SecretKey { get; set; } = default!;

    public int AccessTokenLifeTimeInMinutes { get; set; }

    public int RefreshTokenLifeTimeInDays { get; set; }
}