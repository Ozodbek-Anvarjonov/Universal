namespace Project.Application.Dtos.Login;

public class LoginWithEmailRequest
{
    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
}