using System.Linq.Expressions;

namespace BlogPessoal.Repositories;

public interface IRepository<T>
{
    //CRUD
    T Create(T entity);
    Task<IEnumerable<T>> GetAllAsync();//mais leve (Read)
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);//serve para receber uma função lambda que vai retornar true se encontrar o id oou falso se não encontrar

    T Update(T entity);

    T Delete(T entity);

}
