using BlogPessoal.Data;
using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogPessoal.Repositories.Postagens;

public class PostagemRepository : Repository<Postagem>, IPostagemRepository
{
    public PostagemRepository(BlogDbContext context) : base(context)
    {}
    public override async Task<IEnumerable<Postagem>> GetAllAsync()
    {
        return await _context.Postagens.Include(p => p.Tema).Include(p => p.Usuario).AsNoTracking().ToListAsync();
    }
    public override async Task<Postagem> GetAsync(Expression<Func<Postagem, bool>> predicate)
    {
        return await _context.Postagens.Include(p => p.Tema).Include(p => p.Usuario).FirstOrDefaultAsync(predicate);
    }

    //consulta de autor ou/e tema
    public async Task<PagedResponse<Postagem>> GetFiltroAutorTemaAsync(PostagensFiltroAutorTema postFiltroParams)
    {
        var consulta = await GetAllAsync();

        if (postFiltroParams.AutorId != null)
            consulta = consulta.Where(p => p.UsuarioId == postFiltroParams.AutorId);

        if (postFiltroParams.TemaId != null)
            consulta = consulta.Where(p => p.TemaId == postFiltroParams.TemaId);

        var postOrdenado = consulta.OrderBy(p => p.Data);

        var count = postOrdenado.Count();

        var itensPaginados = postOrdenado
            .Skip((postFiltroParams.PageNumber - 1) * postFiltroParams.PageSize)
            .Take(postFiltroParams.PageSize)
            .ToList();

        return new PagedResponse<Postagem>(
            dados: itensPaginados,
            count: count,
            pageNumber: postFiltroParams.PageNumber,
            pageSize: postFiltroParams.PageSize
        );
    }
}
