using Project.Domain.Enums;
using System.Text.Json.Serialization;

namespace Project.Application.Dtos.Users;

public class UserGetDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? MiddleName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;
}