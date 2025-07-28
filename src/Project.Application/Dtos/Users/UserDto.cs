namespace Project.Application.Dtos.Users;

public class UserDto
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? MiddleName { get; set; }
}