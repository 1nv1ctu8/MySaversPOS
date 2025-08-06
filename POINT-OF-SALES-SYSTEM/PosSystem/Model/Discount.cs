using System;

namespace PosSystem.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public string TransNum { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public int DiscountType { get; set; }
        public string DiscountTypeDesc { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}
