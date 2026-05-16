using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }

    [Required]
    [StringLength(80)]
    public string? UsuarioNome { get; set; }

    [Required]
    public DateTime? UsuarioIdade { get; set; }

    //data de criação?
    public DateTime DataCadastro { get; set; }
}
