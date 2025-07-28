namespace Project.Application.Common.Filters;

public class UserFilter : PaginationFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
}