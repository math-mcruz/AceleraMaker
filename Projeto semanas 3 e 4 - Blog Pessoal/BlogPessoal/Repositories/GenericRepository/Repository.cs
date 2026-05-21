using BlogPessoal.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogPessoal.Repositories.GenericRepository;

public class Repository<T> : IRepository<T> where T : class
{
    //tem que ser protected, pois ela vai ser herdada e private ia dar erro por causa disso
    protected readonly BlogDbContext _context;

    public Repository(BlogDbContext context)
    {
        _context = context;
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        //como esse get vai ser apenas para exibir, usar AsNoTracking vai otimizar a busca
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        //se não achar o id vai retornar null, serve para deletar o tema ou postagem
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public T Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }


}
