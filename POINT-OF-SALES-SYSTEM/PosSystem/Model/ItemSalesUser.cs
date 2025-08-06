using System;

namespace PosSystem.Model
{
    public class ItemSalesUser
    {
        public string TransNum { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public string Cashier { get; set; }
    }
}
