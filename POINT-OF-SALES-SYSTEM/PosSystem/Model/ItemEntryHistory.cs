using System;

namespace PosSystem.Model
{
    public class ItemEntryHistory
    {
        public string ItemCode { get; set; } = string.Empty;
        public decimal MaterialCost { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public int UpdatedBy { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
}
