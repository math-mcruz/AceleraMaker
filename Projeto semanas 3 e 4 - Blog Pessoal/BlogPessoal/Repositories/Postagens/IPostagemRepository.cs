using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;

namespace BlogPessoal.Repositories.Postagens;

public interface IPostagemRepository : IRepository<Postagem>
{
   IEnumerable<Postagem> GetAutorTema(PostagensParameters postagemParams);
}
