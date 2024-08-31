using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.DTO;
public class ClientDto
{
    public int id { get; init; }

    [Required(ErrorMessage = "Campo obrigatório! [name]")]
    [StringLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
    public string name { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [email]")]
    [StringLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string email { get; set; } = "";

    public ICollection<Reservation> reservations { get; set; } = new List<Reservation>();
}