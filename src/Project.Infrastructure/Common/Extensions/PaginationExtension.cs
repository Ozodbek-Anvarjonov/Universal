using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Filters;
using Project.Application.Common.Response;
using Project.Domain.Common.Entities;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Project.Infrastructure.Common.Extensions;

public static class PaginationExtension
{
    private static readonly ConcurrentDictionary<(Type, string), LambdaExpression> _selectorCache = new();

    public static async Task<IEnumerable<TEntity>> ToPaginateAsync<TEntity>(
        this IQueryable<TEntity> source,
        PaginationFilter filter,
        IHeaderWriter? writer = default,
        CancellationToken cancellationToken = default
        )
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (filter == null) throw new ArgumentNullException(nameof(filter));


        if (writer is not null)
        {
            var totalCount = await source.CountAsync();
            var metaData = new PaginationMetaData(totalCount, filter);

            writer.WritePaginationMetaData(metaData);
        }

        source = source
            .ApplyOrdering(filter)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        return await source.ToListAsync(cancellationToken);
    }

    private static IQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> source, PaginationFilter filter)
    {
        var propertyName = filter.OrderBy ?? nameof(IEntity.Id);
        var isDescending = string.Equals(filter.OrderType, "desc", StringComparison.OrdinalIgnoreCase);

        var key = (typeof(TEntity), propertyName.ToLowerInvariant());

        if (!_selectorCache.TryGetValue(key, out var lambda))
        {
            var property = typeof(TEntity).GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                ?? throw new BadRequestException($"Cannot order by '{propertyName}' — property not found.");

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyAccess = Expression.Property(parameter, property);
            lambda = Expression.Lambda(propertyAccess, parameter);

            _selectorCache.TryAdd(key, lambda);
        }

        var methodName = isDescending ? "OrderByDescending" : "OrderBy";

        var expression = Expression.Call(typeof(Queryable), methodName,
            new Type[] { typeof(TEntity), lambda.Body.Type },
            source.Expression,
            Expression.Quote(lambda));

        return source.Provider.CreateQuery<TEntity>(expression);
    }
}