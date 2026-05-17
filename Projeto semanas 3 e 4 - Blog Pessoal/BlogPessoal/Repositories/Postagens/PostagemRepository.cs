using BlogPessoal.Data;
using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories.Postagens;

public class PostagemRepository : Repository<Postagem>, IPostagemRepository
{
    public PostagemRepository(BlogDbContext context) : base(context)
    {}

    //consulta de autor ou/e tema
    public IEnumerable<Postagem> GetAutorTema(PostagensParameters postagemParams)
    {
        //faz o include para ter acesso ao nome dos usuarios e temas
        var consulta = _context.Set<Postagem>().Include(p => p.Usuario).Include(p => p.Tema).AsQueryable();

        if (postagemParams.Autor != null)
            consulta = consulta.Where(p => p.UsuarioId == postagemParams.Autor);

        if (postagemParams.Tema != null)
            consulta = consulta.Where(p => p.TemaId == postagemParams.Tema);

        //ordenada por data de postagem
        return consulta.OrderBy(p => p.Data).Skip((postagemParams.PageNumber - 1) * postagemParams.PageSize).Take(postagemParams.PageSize).ToList();
    }
}
