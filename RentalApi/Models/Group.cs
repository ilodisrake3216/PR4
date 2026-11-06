namespace LanguageLearnApi.Models;

public class Group
{
    public int Id { get; set; }
    public string Language { get; set; } = "";
    public List<int> Listners { get; set; } = new();
    public string Teacher { get; set; } = "";
    public decimal Rating { get; set; } = 1.0m;
    public int? StarUser { get; set; }
    public string? Link { get; set; }
}