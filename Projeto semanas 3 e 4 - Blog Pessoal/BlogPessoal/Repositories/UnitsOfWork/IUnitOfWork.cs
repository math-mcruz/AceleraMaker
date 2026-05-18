using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Temas;
using BlogPessoal.Repositories.Usuarios;

namespace BlogPessoal.Repositories.UnitsOfWork;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    IPostagemRepository PostagemRepository { get; }
    ITemaRepository TemaRepository { get; }
    Task CommitAsync();
}
