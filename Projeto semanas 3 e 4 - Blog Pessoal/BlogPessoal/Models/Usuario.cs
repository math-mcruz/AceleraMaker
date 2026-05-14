using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BlogPessoal.Models;

[Table("Usuarios")]
public class Usuario
{
    public Usuario()
    {
        Postagem = new Collection<Postagem>();
    }
    // -------------------------------------------------------- Assistir aula Data Annotations validações (AULA 60) ----------------------------------
    [Key]
    public int UsuarioId { get; set; }

    [Required]
    [StringLength(80)]
    public string? UsuarioNome { get; set; }

    [Required]
    public DateTime? UsuarioIdade { get; set; }

    //data de criação?
    public DateTime DataCadastro { get; set; }
    //preencher com mais dados

    //Usuario 1:N Postagem
    public ICollection<Postagem>? Postagem { get; set; }
}
