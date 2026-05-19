namespace BlogPessoal.DTOs.Postagens;

public class PostagemResponseDTO
{
    public int PostagemId { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public DateTime? Data { get; set; }
    public string? NomeAutor { get; set; }
    public string? NomeTema { get; set; }
}
