using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs;

public class TemaDTO
{
    public int TemaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
}
