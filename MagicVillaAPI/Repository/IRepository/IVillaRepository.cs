using System.Linq.Expressions;
using MagicVillaAPI.Models.VillaModel;

namespace MagicVillaAPI.Repository.IRepository

{
    public interface IVillaRepository : IRepositorys<VillaModel>
    {


        Task<VillaModel> UpdateAsync(VillaModel entity);


    }
}