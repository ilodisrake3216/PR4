namespace LanguageLearnApi.Models;

/// <summary>Страница данных и общее количество записей.</summary>
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}