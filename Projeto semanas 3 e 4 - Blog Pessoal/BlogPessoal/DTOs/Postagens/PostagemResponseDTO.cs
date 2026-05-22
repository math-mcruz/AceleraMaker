using System.Text.Json.Serialization;

namespace BlogPessoal.DTOs.Postagens;

public class PostagemResponseDTO
{
    public int PostagemId { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public DateTime? Data { get; set; }
    public int UsuarioId { get; set; }
    public string? NomeAutor { get; set; }
    public int TemaId { get; set; }
    public string? NomeTema { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Resumo { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Tags { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Categoria { get; set; }
}
