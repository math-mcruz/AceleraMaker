using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Temas;

namespace BlogPessoal.Repositories.UnitsOfWork;

public interface IUnitOfWork
{
    IPostagemRepository PostagemRepository { get; }
    ITemaRepository TemaRepository { get; }
    Task CommitAsync();
}
