using System.ComponentModel.DataAnnotations;

namespace LanguageLearnApi.Models;

public class CreateGroupRequest
{
    [Required(ErrorMessage = "Укажите название языка")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название языка должно быть от 2 до 100 символов")]
    public string Language { get; set; } = "";

    [Required(ErrorMessage = "Укажите преподавателя")]
    public string Teacher { get; set; } = "";

    [Range(1.0, 5.0, ErrorMessage = "Рейтинг должен быть в диапазоне от 1.0 до 5.0")]
    public decimal Rating { get; set; } = 1.0m;
    public string? Link { get; set; }
}