namespace LanguageSchool;

public class Group
{
    public int Id { get; set; }
    public string Language { get; set; } = "";
    public string Teacher { get; set; } = "";
    public decimal Rating { get; set; }
    public string? Link { get; set; }
    public List<int> Listners { get; set; } = new();
}

public class GroupListItem
{
    public int Id { get; set; }
    public string Language { get; set; } = "";
    public string Teacher { get; set; } = "";
    public decimal Rating { get; set; }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
