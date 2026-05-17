using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs;

public class PostagemDTO
{
    public int PostagemId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Titulo { get; set; }

    [Required]
    [StringLength(100)]
    public string? Texto { get; set; }
    [Required]
    public DateTime? Data { get; set; }
    public int UsuarioId { get; set; }
    public int TemaId { get; set; }
}
