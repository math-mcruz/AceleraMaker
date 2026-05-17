using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Usuarios;

public class UsuarioRequestDTO
{

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [StringLength(30)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [StringLength(20)]
    [EmailAddress]
    public string Email { get; set; }

    //data de criação?
    [Required(ErrorMessage = "Senha é obrigatório.")]
    [StringLength(20, MinimumLength = 8)]
    public string Senha { get; set; }
}
