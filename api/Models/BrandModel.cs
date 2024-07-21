namespace api.Models
{
    public class BrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}
