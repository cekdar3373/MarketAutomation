using System;
using System.Collections.Generic;

namespace MarketAutomation.Models
{
    public class Sale
    {
        public List<CartItem> Items { get; private set; }
        public DateTime SaleDate { get; private set; }

        public Sale()
        {
            Items = new List<CartItem>();
            SaleDate = DateTime.Now;
        }

        public void AddItem(CartItem item)
        {
            Items.Add(item);
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (var item in Items)
            {
                total += item.GetTotalPrice();
            }

            return total;
        }
    }
}
