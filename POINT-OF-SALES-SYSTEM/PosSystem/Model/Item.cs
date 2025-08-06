namespace PosSystem.Model
{
    public class Item
    {
        public string ItemCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public string LongDescription { get; set; } = string.Empty;
        public decimal MaterialCost { get; set; }
        //public int QuickPicks { get; set; }
        //public int CategoryId { get; set; }
        //public bool Editable { get; set; }
        //public bool Inactive { get; set; }
        public string Units { get; set; } = string.Empty;
        //public string MbFlag { get; set; } = string.Empty;
        //public string ImageFilename { get; set; } = string.Empty;
        //public int SalesAccount { get; set; }
        //public int CogsAccount { get; set; }
        public int InventorySold { get; set; }
        //public int AdjustmentAccount { get; set; }
        public int TaxTypeId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int InventoryQuantity { get; set; }
        //public string InventoryLocation { get; set; } = string.Empty;
        public decimal Retail { get; set; }
        //public decimal Wholesale { get; set; }
        //public int Employee { get; set; }
    }
}
