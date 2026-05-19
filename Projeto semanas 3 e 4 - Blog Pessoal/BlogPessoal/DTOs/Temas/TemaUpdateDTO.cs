using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Temas;

public class TemaUpdateDTO : TemaRequestDTO
{
    [Required]
    public int TemaId { get; set; }
}
