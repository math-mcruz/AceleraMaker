using BlogPessoal.Data;
using BlogPessoal.Models;
using BlogPessoal.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories.Temas;

//vou fazer primeiro no temas pois é o mais simples
public class TemaRepository : Repository<Tema>, ITemaRepository
{
    private readonly BlogDbContext _context;

    public TemaRepository(BlogDbContext context) : base(context)
    { }
}
