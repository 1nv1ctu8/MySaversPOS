using System;

namespace PosSystem.Model
{
    public class Sales
    {
        public string TransNum { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal VatPercent { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DiscountDescription { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Cashier { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
    }
}
