using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.Models;

[Table("Postagens")]
public class Postagem
{
    [Key]
    public int PostagemId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Titulo { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? Texto { get; set; }
    [Required]
    public DateTime? Data { get; set; }

    //declarando as dependencias: Usuario 1:N Postagem e Tema 1:N Postagem.
    public int UsuarioId { get; set; }
    public int TemaId { get; set; }
    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
    [JsonIgnore]
    public virtual Tema? Tema { get; set; }

}
