using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs;

public class PostagemDTO
{
    public int PostagemId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
}
