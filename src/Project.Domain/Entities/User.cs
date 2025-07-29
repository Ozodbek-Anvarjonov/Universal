using Project.Domain.Common.Entities;
using Project.Domain.Enums;

namespace Project.Domain.Entities;

public class User : SoftDeletedEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? MiddleName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string Password { get; set; } = default!;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;
}