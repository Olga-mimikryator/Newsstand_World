namespace Newsstand_World.Model
{
    public class Product : EFModel
    {
        public int TypeID { get; set; }
        public int PublisherID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
