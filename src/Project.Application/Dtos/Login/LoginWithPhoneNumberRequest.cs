namespace Project.Application.Dtos.Login;

public class LoginWithPhoneNumberRequest
{
    public string PhoneNumber { get; set; } = default!;

    public string Password { get; set; } = default!;
}