using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_Project.Models
{
    public abstract class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public ICollection<Media> Media { get; set; } = new List<Media>();

        public Manufacturer? Manufacturer { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerID { get; set; }

        public int Warranty { get; set; }

        [Required]
        public int UnitInStock { get; set; }

        public string? Description { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
