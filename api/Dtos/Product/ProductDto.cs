using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public Guid BrandId { get; set; }
    }
}
