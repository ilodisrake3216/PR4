using System.ComponentModel.DataAnnotations;

namespace LanguageLearnApi.Models;

public class PingGroupRequest
{
    [Required(ErrorMessage = "Текст оповещения обязателен для заполнения")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Сообщение не должно превышать 500 символов")]
    public string Message { get; set; } = "";
}