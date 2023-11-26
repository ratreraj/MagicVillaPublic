

using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int Sqft { get; set; }

        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        [Required]
        public string ImagUrl { get; set; }
        public string Amenity { get; set; }
    }
}