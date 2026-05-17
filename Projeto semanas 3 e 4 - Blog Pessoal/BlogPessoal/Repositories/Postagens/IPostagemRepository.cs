using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;

namespace BlogPessoal.Repositories.Postagens;

public interface IPostagemRepository : IRepository<Postagem>
{
   PagedList<Postagem> GetFiltroAutorTema(PostagensFiltroAutorTema postagemFiltroParams);
}
