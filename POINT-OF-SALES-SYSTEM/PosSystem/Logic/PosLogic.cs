using PosSystem.Model;
using PosSystem.Repository;
using System.Collections.Generic;

namespace PosSystem.Logic
{
    public class PosLogic
    {
        readonly PosRepository _posRepository = new PosRepository();

        public List<Purchase> GetPurchaseInfoByTransNumAndStatus(string transNum, decimal vatPercent, int status = 0)
        {
            var rawPurchases = _posRepository.GetPurchaseByTransNumAndStatus(transNum, status);

            List<Purchase> purchases = new List<Purchase>();

            foreach (var purchase in rawPurchases)
            {
                var totalPrice = purchase.Price * purchase.Quantity;
                purchase.DiscountAmount = 0;

                if (purchase.DiscountPercent > 0)
                {
                    var discountType = _posRepository.GetDiscountTypeByDiscountId(purchase.DiscountType);
                    purchase.DiscountDesc = discountType.DiscountDesc;
                    purchase.DiscountAmount = totalPrice * purchase.DiscountPercent / 100;
                }

                if (purchase.Vattable == "Yes")
                {
                    purchase.VatPercent = vatPercent * 100;
                    purchase.VatAmount = (totalPrice - purchase.DiscountAmount) * vatPercent;                    
                }

                purchase.Total = totalPrice;

                purchases.Add(purchase);
            }

            return purchases;
        }
    }
}
