using System.Linq.Expressions;

namespace MagicVillaAPI.Repository.IRepository
{


    public interface IRepositorys<T> where T : class
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> fillter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> fillter = null, bool tracked = true);
        Task Create(T entity);
        //Task Update(T entity);
        Task Remove(T entity);
        Task Save();

    }

}