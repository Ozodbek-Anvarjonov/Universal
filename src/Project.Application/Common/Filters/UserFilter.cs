using Project.Domain.Enums;

namespace Project.Application.Common.Filters;

public class UserFilter : PaginationFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }

    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }

    public UserRole? Role { get; set; }
    public bool? IsActive { get; set; }
}