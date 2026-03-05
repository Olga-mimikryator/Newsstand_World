using System;
using System.Data;

namespace Newsstand_World.Model
{
    public class Purchaser : EFModel
    {
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public DateTime BirthDate { get; set; }
    }
}
