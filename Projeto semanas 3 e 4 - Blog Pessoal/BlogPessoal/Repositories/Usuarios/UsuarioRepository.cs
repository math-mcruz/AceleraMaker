using BlogPessoal.Data;
using BlogPessoal.Models;

namespace BlogPessoal.Repositories.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BlogDbContext _context;

    public UsuarioRepository(BlogDbContext context)
    {
        _context = context;
    }
    public Usuario CadastrarUsuario()
    {
        throw new NotImplementedException();
    }
    public Usuario AtualizarUsuario()
    {
        throw new NotImplementedException();
    }

    public Usuario DeletarUsuario()
    {
        throw new NotImplementedException();
    }
}
