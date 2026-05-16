namespace BlogPessoal.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    IPostagemRepository PostagemRepository { get; }
    ITemaRepository TemaRepository { get; }

    void Commit();
    void Dispose();
}
