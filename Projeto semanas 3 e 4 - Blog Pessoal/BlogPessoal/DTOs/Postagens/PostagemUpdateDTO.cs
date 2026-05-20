using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Postagens
{
    public class PostagemUpdateDTO
    {
        public int PostagemId { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }
        public int TemaId { get; set; }
    }
}
