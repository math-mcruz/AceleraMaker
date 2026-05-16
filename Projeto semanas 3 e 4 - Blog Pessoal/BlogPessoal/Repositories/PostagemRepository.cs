using BlogPessoal.Data;
using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public class PostagemRepository : Repository<Postagem>, IPostagemRepository
{
    private readonly BlogDbContext _context;

    public PostagemRepository(BlogDbContext context) : base(context)
    {}
}
