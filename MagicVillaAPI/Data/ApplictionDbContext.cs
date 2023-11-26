using MagicVillaAPI.Models.VillaModel;

using Microsoft.EntityFrameworkCore;

namespace MagicVillaModelAPI.Data
{
    

    public  class ApplictionDbContext:DbContext
    {

        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options):base(options)
        {
            
        }
        public DbSet<VillaModel> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<VillaModel>().HasData(
                    new VillaModel
              {
                  Id=1,
                  Name = "Royal VillaModel",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImagUrl = "https://dotnetmasteryimages.blob.core.windows.net/blueVillaModelimages/VillaModel3.jpg",
                  Occupancy = 4,
                  Rate = 200,
                  Sqft = 550,
                  Amenity = "",
                  CreatedDate= DateTime.Now
              },
              new VillaModel
              {
                  Id = 2,
                  Name = "Premium Pool VillaModel",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImagUrl = "https://dotnetmasteryimages.blob.core.windows.net/blueVillaModelimages/VillaModel1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  Sqft = 550,
                  Amenity="",
                  CreatedDate= DateTime.Now
              },
              new VillaModel
              {
                  Id = 3,
                  Name = "Luxury Pool VillaModel",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImagUrl = "https://dotnetmasteryimages.blob.core.windows.net/blueVillaModelimages/VillaModel4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  Sqft = 750,
                  Amenity = "",
                  CreatedDate= DateTime.Now
              },
              new VillaModel
              {
                  Id = 4,
                  Name = "Diamond VillaModel",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImagUrl = "https://dotnetmasteryimages.blob.core.windows.net/blueVillaModelimages/VillaModel5.jpg",
                  Occupancy = 4,
                  Rate = 550,
                  Sqft = 900,
                  Amenity = "",
                  CreatedDate= DateTime.Now
              },
              new VillaModel
              {
                  Id = 5,
                  Name = "Diamond Pool VillaModel",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImagUrl = "https://dotnetmasteryimages.blob.core.windows.net/blueVillaModelimages/VillaModel2.jpg",
                  Occupancy = 4,
                  Rate = 600,
                  Sqft = 1100,
                  Amenity = "",
                  CreatedDate= DateTime.Now
              }

                );
        }
        
    }
}