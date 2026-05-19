using BlogPessoal.Data;
using BlogPessoal.DTOs.Postagens;
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
    public async Task<PagedList<Postagem>> GetFiltroAutorTemaAsync(PostagensFiltroAutorTema postFiltroParams)
    {
        //faz o include para ter acesso ao nome dos usuarios e temas ------------------------------------**********************************
        var consulta = await GetAllAsync();

        if (postFiltroParams.AutorId != null)
            consulta = consulta.Where(p => p.UsuarioId == postFiltroParams.AutorId);

        if (postFiltroParams.TemaId != null)
            consulta = consulta.Where(p => p.TemaId == postFiltroParams.TemaId);

        //ordenada por data de postagem
        var postOredenado = consulta.OrderBy(p => p.Data).AsQueryable();

        return PagedList<Postagem>.ToPagedList(postOredenado, postFiltroParams.PageNumber, postFiltroParams.PageSize);
    }

    //public async Task<Postagem> GetPostagemCompletaAsync(int id)
    //{
    //    return await _context.Postagens.Include(p => p.Tema).Include(p => p.Usuario).FirstOrDefaultAsync(p => p.PostagemId == id);
    //}
}
