using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Postagens;

public class PostagemRequestDTO
{

    [Required(ErrorMessage = "Título é obrigatório.")]
    [StringLength(80)]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "Texto é obrigatório.")]
    [StringLength(100, MinimumLength =1)]
    public string? Texto { get; set; }

    //[Required(ErrorMessage = "Data é obrigatório.")]
    //achar uma maneira de colocar data automatico ------------------------------------***********************************
    public DateTime? Data { get; set; }

    [Required(ErrorMessage = "Id do Tema é obrigatório.")]
    public int TemaId { get; set; }
}
