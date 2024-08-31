using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO;
public class ReservationDto
{
    public int id { get; init; }

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(10, ErrorMessage = "O campo {0} não pode ter mais que 10 caracteres")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "A data deve conter o formato: yyyy-MM-dd.")]
    public string reservation_date { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(8, ErrorMessage = "O campo {0} não pode ter mais que 8 caracteres")]
    [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "O campo deve conter o formato: HH:mm:ss.")]
    public string initial_hour { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(8, ErrorMessage = "O campo {0} não pode ter mais que 8 caracteres")]
    [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "O campo deve conter o formato: HH:mm:ss.")]
    public string finish_hour { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "O campo {0} não pode ter mais que 100 caracteres")]
    public string protocol { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "O campo {0} não pode ter mais que 100 caracteres")]
    public string status { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    public int client_id { get; init; }
    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    public int room_id { get; set; }
}