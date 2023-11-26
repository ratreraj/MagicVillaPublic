
using MagicVillaAPI.Models.DTO;

namespace MagicVillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
                new VillaDTO { Id=101,Name="Pool Villa",Sqft=300,Occupancy=4},
                new VillaDTO { Id=102,Name="Beach Villa",Sqft=100,Occupancy=3}
        };

    }
}
