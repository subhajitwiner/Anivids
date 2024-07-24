namespace api.Dtos.Product
{
    public class ProductInputDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public Guid BrandId { get; set; }
    }
}
