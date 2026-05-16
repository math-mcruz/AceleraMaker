using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories;

//vou fazer primeiro no temas pois é o mais simples
public class TemaRepository : ITemaRepository
{
    private readonly BlogDbContext _context;

    public TemaRepository(BlogDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Tema> listarTemas() => _context.Temas.ToList();
    //public IEnumerable<Tema> listarTemas()
    //{
    //    return _context.Temas.ToList();
    //}
    public Tema CriarTema(Tema tema)
    {
        if(tema is null)
            throw new ArgumentNullException(nameof(tema));

        _context.Temas.Add(tema);
        _context.SaveChanges();

        return tema;
    }
    public Tema AtualizarTema(Tema tema)
    {
        if (tema is null)
            throw new ArgumentNullException(nameof(tema));

        _context.Entry(tema).State = EntityState.Modified;
        _context.SaveChanges();

        return tema;
    }

    public Tema DeletarTema(int id)
    {
        var tema = _context.Temas.Find(id);

        if (tema is null)
            throw new ArgumentNullException(nameof(tema));
        _context.Temas.Remove(tema);
        _context.SaveChanges();
        return tema;
    }

}
