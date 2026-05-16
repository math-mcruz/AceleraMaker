using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.Models;

[Table("Temas")]
public class Tema
{
    public Tema()
    {
        Postagem = new Collection<Postagem>();
    }

    [Key]
    public int TemaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    //Tema 1:N Postagem.
    [JsonIgnore]
    public ICollection<Postagem>? Postagem { get; set; }
}
