

using AutoMapper;
using MagicVillaAPI.Models.DTO;
using MagicVillaAPI.Models.VillaModel;

namespace MagicVillaAPI
{
    
     class MappingConfig:Profile
     {
         public MappingConfig()
         {
            CreateMap<VillaModel,VillaDTO>().ReverseMap();
            CreateMap<VillaModel,VillaCreateDTO>().ReverseMap();
            CreateMap<VillaModel,VillaUpdateDTO>().ReverseMap();
            
         }
     }

}