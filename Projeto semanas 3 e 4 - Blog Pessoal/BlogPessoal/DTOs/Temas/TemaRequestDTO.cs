using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs;

public class TemaRequestDTO
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [StringLength(80, MinimumLength = 1)]
    public string? Nome { get; set; }
}
