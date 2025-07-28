namespace Project.Application.Common.Response;

public interface IHeaderWriter
{
    void WritePaginationMetaData(PaginationMetaData paginationMetaData);
}