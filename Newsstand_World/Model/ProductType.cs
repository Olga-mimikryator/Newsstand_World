namespace Newsstand_World.Model
{
    public class ProductType : EFModel
    {
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
