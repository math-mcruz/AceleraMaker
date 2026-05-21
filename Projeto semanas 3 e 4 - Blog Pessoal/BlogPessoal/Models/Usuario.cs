using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Models;

[Table("Usuarios")]
public class Usuario : IdentityUser<int> //int para deixar o id herdado de tipo int
{
    public Usuario()
    {
        Postagem = new Collection<Postagem>();
    }
    //Id, UserName, Email e SenhaHash ja estão em identityUser

    // Relação 1:N com Postagem
    public ICollection<Postagem>? Postagem { get; set; }
}
