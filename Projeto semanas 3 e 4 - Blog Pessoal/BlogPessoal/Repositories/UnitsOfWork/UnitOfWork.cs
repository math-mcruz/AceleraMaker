using BlogPessoal.Data;
using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Temas;
using BlogPessoal.Repositories.Usuarios;

namespace BlogPessoal.Repositories.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IUsuarioRepository? _usuarioRepository;

    public IPostagemRepository? _postagemRepository;

    public ITemaRepository? _temaRepository;

    public BlogDbContext _context;

    public UnitOfWork(BlogDbContext context)
    {
        _context = context;
    }

    public IUsuarioRepository UsuarioRepository
    {
        get 
        {
            //verifica se tem uma instancia de usuario se não tiver ele cria
            //pode usar o operador ??, mas dessa maneira é mais facil de entender
            if(_usuarioRepository is null)
            {
                _usuarioRepository = new UsuarioRepository(_context); 
            }
            return _usuarioRepository;
        }
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

    public void Commit() //salvar no banco de dados
    {
        _context.SaveChanges();
    }

    public void Dispose()//liberação de recursos
    {
        _context.Dispose();
    }

}
