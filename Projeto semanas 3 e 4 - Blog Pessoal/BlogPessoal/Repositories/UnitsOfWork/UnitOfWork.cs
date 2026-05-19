using BlogPessoal.Data;
using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Temas;

namespace BlogPessoal.Repositories.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IPostagemRepository? _postagemRepository;

    public ITemaRepository? _temaRepository;

    public BlogDbContext _context;

    public UnitOfWork(BlogDbContext context)
    {
        _context = context;
    }
    public IPostagemRepository PostagemRepository
    {
        get 
        {
            //verifica se tem uma instancia de postagem se não tiver ele cria
            if (_postagemRepository is null)
            {
                _postagemRepository = new PostagemRepository(_context);
            }
            return _postagemRepository;
        }
    }
    public ITemaRepository TemaRepository
    {
        get 
        {
            //verifica se tem uma instancia de tema se não tiver ele cria
            if (_temaRepository is null)
            {
                _temaRepository = new TemaRepository(_context);
            }
            return _temaRepository;
        }
    }

    public async Task CommitAsync() //salvar no banco de dados
    {
        await _context.SaveChangesAsync();
    }
}
