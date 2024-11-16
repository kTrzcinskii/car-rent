namespace AppBrowser.DTOs;

public class PaginatedDto<T>
{
    public List<T> Data { get; set; }
    public int Count => Data.Count;
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}