using AppBrowser.DTOs;

namespace AppBrowser.Services.Interfaces;

public interface IPaginationService
{
    PaginatedDto<T> GetPaginatedResponse<T>(IEnumerable<T> source, int page, int pageSize);
}