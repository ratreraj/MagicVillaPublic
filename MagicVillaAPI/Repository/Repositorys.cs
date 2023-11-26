using System.Linq.Expressions;
using MagicVillaAPI.Repository.IRepository;
using MagicVillaModelAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repository
{

    class Repositorys<T> : IRepositorys<T> where T : class
    {
        private readonly ApplictionDbContext _dbContext;
        internal DbSet<T> _dbSet;

        public Repositorys(ApplictionDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> fillter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (fillter != null)
            {
                query = query.Where(fillter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> fillter = null)
        {
            IQueryable<T> query = _dbSet;

            if (fillter != null)
            {
                query = query.Where(fillter);
            }

            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            _dbContext.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
            await Save();
        }


    }

}