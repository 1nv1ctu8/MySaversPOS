using System;

namespace PosSystem.Model
{
    public class CardDisplay
    {
        public string TransNum { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public int Last4Digit { get; set; }
        public decimal Total { get; set; }
        public string Cashier { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}
