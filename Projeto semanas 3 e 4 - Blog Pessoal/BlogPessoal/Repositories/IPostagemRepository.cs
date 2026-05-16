using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public interface IPostagemRepository : IRepository<Postagem>
{
   // filtrar postagens por tema ou/e autor, como fazer?
   // IEnumerable<Postagem> listarPostagens();
}
