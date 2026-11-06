using System.ComponentModel.DataAnnotations;

namespace LanguageLearnApi.Models;
public class AddParticipantRequest
{
    [Required(ErrorMessage = "Укажите идентификатор слушателя")]
    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть больше 0")]
    public int ParticipantId { get; set; }
}