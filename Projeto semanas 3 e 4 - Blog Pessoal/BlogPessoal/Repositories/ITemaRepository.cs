using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public interface ITemaRepository
{
    IEnumerable<Tema> listarTemas();
    Tema CriarTema(Tema tema);
    Tema AtualizarTema(Tema tema);
    Tema DeletarTema(int id);

}
