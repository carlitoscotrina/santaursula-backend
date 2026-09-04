using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class LoginDto
{
    [Required]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
