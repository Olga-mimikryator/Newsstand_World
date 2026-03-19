namespace Newsstand_World.Model
{
    public class Product : EFModel
    {
        public ProductType Type { get; set; } = new();
        public Publisher Publisher { get; set; } = new();
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
