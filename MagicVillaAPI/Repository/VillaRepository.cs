using System.Linq.Expressions;
using MagicVillaAPI.Models.VillaModel;
using MagicVillaAPI.Repository.IRepository;
using MagicVillaModelAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repository
{
    class VillaRepository : Repositorys<VillaModel>, IVillaRepository
    {
        private readonly ApplictionDbContext _dbContext;

        public VillaRepository(ApplictionDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task<VillaModel> UpdateAsync(VillaModel entity)
        {

            entity.CreatedDate = DateTime.Now;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }


    }
}