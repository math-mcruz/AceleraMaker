using BlogPessoal.Data;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories.Postagens;

public class PostagemRepository : Repository<Postagem>, IPostagemRepository
{
    public PostagemRepository(BlogDbContext context) : base(context)
    {}

    //consulta de autor ou/e tema
    public PagedList<Postagem> GetFiltroAutorTema(PostagensFiltroAutorTema postFiltroParams)
    {
        //faz o include para ter acesso ao nome dos usuarios e temas ------------------------------------**********************************
        //var consulta = _context.Set<Postagem>().AsNoTracking().AsQueryable();
        var consulta = GetAll().AsQueryable();

        if (postFiltroParams.AutorId != null)
            consulta = consulta.Where(p => p.UsuarioId == postFiltroParams.AutorId);

        if (postFiltroParams.TemaId != null)
            consulta = consulta.Where(p => p.TemaId == postFiltroParams.TemaId);

        //ordenada por data de postagem
        var postOredenado = consulta.OrderBy(p => p.Data);

        return PagedList<Postagem>.ToPagedList(postOredenado, postFiltroParams.PageNumber, postFiltroParams.PageSize);
    }
}
