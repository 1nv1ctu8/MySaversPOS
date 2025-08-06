using System;

namespace PosSystem.Model
{
    public class GCashMayaDisplay
    {
        public string TransNum { get; set; } = string.Empty;
        public string CellNum { get; set; } = string.Empty;
        public string ReferenceNum { get; set; }
        public decimal Total { get; set; }
        public string Cashier { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}
