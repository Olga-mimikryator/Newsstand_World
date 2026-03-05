namespace Newsstand_World.Model
{
    public class ProductType : EFModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
