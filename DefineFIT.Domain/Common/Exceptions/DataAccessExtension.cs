using Microsoft.EntityFrameworkCore;

namespace DefineFIT.Domain.Common.Extensions;

public static class DataAccessExtension
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
          params string[]? includes) where T : class =>
        includes != null ?
            includes.Aggregate(query, (current, include) => current.Include(include))
            : query;
}