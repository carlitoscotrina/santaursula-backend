using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class SetPasswordDto
{
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
