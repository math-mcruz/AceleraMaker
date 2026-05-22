using BlogPessoal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Data;

public class BlogDbContext: IdentityDbContext<Usuario, IdentityRole<int>, int>
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options): base(options)
    {}

    public DbSet<Postagem>? Postagens { get; set; }
    public DbSet<Tema>? Temas { get; set; }
}
