using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO;
public class RoomDto
{
    public int id { get; init; }

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "O Campo {0} não pode ter mais que 100 caracteres")]
    public string name { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "O Campo {0} não pode ter mais que 100 caracteres")]
    public string description { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    public int capacity { get; set; }

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    public int category_id { get; set; }
}
