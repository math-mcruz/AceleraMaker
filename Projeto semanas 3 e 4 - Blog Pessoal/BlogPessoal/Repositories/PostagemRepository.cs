using BlogPessoal.Data;
using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public class PostagemRepository : IPostagemRepository
{
    private readonly BlogDbContext _context;

    public PostagemRepository(BlogDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Postagem> listarPostagens()
    {
        throw new NotImplementedException();
    }
    public Postagem CriarPostagem()
    {
        throw new NotImplementedException();
    }

    public Postagem AtualizarPostagem()
    {
        throw new NotImplementedException();
    }

    public Postagem DeletarPostagem()
    {
        throw new NotImplementedException();
    }
}
