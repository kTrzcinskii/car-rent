using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class PaginationService : IPaginationService
{
    public PaginatedDto<T> GetPaginatedResponse<T>(IEnumerable<T> source, int page, int pageSize)
    {
        var totalCount = source.Count();
        var data = source
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedDto<T>
        {
            Data = data,
            HasNextPage = ((page + 1) * pageSize) < totalCount,
            HasPreviousPage = page > 0
        };
    }
}