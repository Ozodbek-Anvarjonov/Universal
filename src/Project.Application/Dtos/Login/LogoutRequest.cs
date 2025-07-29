namespace Project.Application.Dtos.Login;

public class LogoutRequest
{
    public string RefreshToken { get; set; } = default!;
}