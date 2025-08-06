using System;

namespace PosSystem.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public string TransNum { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public int TaxTypeId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Status { get; set; }
        public int Cashier { get; set; }
        public string CashierFirstName { get; set; } = string.Empty;
        public string CashierLastName { get; set; } = string.Empty;
    }
}
