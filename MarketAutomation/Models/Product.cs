using System.Diagnostics;
using System.Xml.Linq;

namespace MarketAutomation.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price:C} (Stok: {Stock})";
        }
            
    
    }

}


