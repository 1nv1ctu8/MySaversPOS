using System;

namespace PosSystem.Model
{
    public class Purchase
    {
        public int CartId { get; set; }
        public string TransNum { get; set;} = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
        public int DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DiscountDesc { get; set; } = string.Empty;
        public string Vattable { get; set; } = string.Empty;
        public decimal VatPercent { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; } = string.Empty;
        public int CashierId { get; set; }
        public string CashierFName { get; set; } = string.Empty;
        public string CashierLName { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
    }
}
