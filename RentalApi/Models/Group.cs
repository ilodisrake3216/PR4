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
    public const int MaxListenersCount = 15;

    public void AddListener(int listener)
    {
        if (listener < 1 || listener > 10000)
        {
            throw new ArgumentException("Не существует слушателя с таким номером");
        }

        if (Listners.Count >= MaxListenersCount)
        {
            throw new InvalidOperationException($"В группе не может быть больше {MaxListenersCount} слушателей.");
        }

        Listners.Add(listener);
    }
}