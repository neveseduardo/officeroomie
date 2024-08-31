using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO;
public class RoomCategoryDto
{
    public int id { get; init; }

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "O campo {0} não pode ter mais que 100 caracteres")]
    public string description { get; init; } = "";
}