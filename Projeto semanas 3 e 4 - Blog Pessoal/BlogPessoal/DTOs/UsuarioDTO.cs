using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }

    [Required]
    [StringLength(30)]
    public string Nome { get; set; }

    [Required]
    [StringLength(20)]
    public string Email { get; set; }

    //data de criação?
    [Required]
    [StringLength(20)]
    public string Senha { get; set; }
}
