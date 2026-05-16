using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Data;

public class BlogDbContext: DbContext
{
    //faz o mapeamento
    public BlogDbContext(DbContextOptions<BlogDbContext> options): base(options)
    {}

    public DbSet<Usuario>? Usuarios { get; set; }
    public DbSet<Postagem>? Postagens { get; set; }
    public DbSet<Tema>? Temas { get; set; }

   // public DbSet<UsuarioLogin> UsuariosLogins { get; set; }//não é opcional
}
