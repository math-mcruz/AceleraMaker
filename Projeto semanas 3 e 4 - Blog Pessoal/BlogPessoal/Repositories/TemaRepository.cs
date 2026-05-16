using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories;

//vou fazer primeiro no temas pois é o mais simples
public class TemaRepository : Repository<Tema>, ITemaRepository
{
    private readonly BlogDbContext _context;

    public TemaRepository(BlogDbContext context) : base(context)
    { }
}
