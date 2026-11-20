namespace LanguageLearnApi.Models;

/// <summary>Данные учебной группы для отображения в списке.</summary>
public class GroupListItem
{
    public int Id { get; set; }
    public string Language { get; set; } = "";
    public string Teacher { get; set; } = "";
    public decimal Rating { get; set; }
}