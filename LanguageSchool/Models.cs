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
