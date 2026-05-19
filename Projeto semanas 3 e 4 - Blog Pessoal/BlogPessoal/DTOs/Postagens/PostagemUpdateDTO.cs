using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Postagens
{
    public class PostagemUpdateDTO : PostagemRequestDTO
    {
        [Required]
        public int PostagemId { get; set; }
    }
}
