using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public interface IPostagemRepository
{
    Postagem CriarPostagem();
    Postagem AtualizarPostagem();
    Postagem DeletarPostagem();
    IEnumerable<Postagem> listarPostagens();

   // filtrar postagens por tema ou/e autor, como fazer?
   // IEnumerable<Postagem> listarPostagens();

}
