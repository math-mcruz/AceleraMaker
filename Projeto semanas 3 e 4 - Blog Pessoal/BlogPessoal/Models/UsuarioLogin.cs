using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Models;

[NotMapped]
public class UsuarioLogin
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
}
