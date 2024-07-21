using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public Guid BrandId { get; set; }

        // Navigation property
        [ForeignKey("BrandId")]
        public BrandModel Brand { get; set; }

    }
}
