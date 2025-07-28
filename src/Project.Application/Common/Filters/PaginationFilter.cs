using Project.Domain.Common.Entities;

namespace Project.Application.Common.Filters;

public class PaginationFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string OrderBy { get; set; } = nameof(IEntity.Id);
    public string OrderType { get; set; } = "desc";
}