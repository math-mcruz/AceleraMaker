using System.Linq.Expressions;

namespace BlogPessoal.Repositories.GenericRepository;

public interface IRepository<T>
{
    //CRUD
    T Create(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);//serve para receber uma função lambda que vai retornar bool

    T Update(T entity);

    T Delete(T entity);

}
