using PosSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PosSystem.Logic
{
    public class ReceiptLogic
    {
        private readonly string _posDashCount = ConfigurationManager.AppSettings["PosDashCount"];
        private readonly string _lineNumCharsConfig = ConfigurationManager.AppSettings["LineNumChars"];
        private int _lineNumChars;

        private string CenterLine(string text, int lineNumChars = 40)
        {
            var textCount = text.Trim().Length;
            var newText = Environment.NewLine;
            var lineToStart = (lineNumChars / 2) - (textCount / 2);
            newText += new string(' ', lineToStart) + text;
            return newText;
        }

        private string DashDivider(int times)
        {
            var text = Environment.NewLine;
            text += new string('-', times);
            return text;
        }

        private string SingleLine(string text)
        {
            return Environment.NewLine + text;
        }

        private string JustifyTwoTexts(string text1, string text2, int lineNumChars = 40)
        {
            var newText = Environment.NewLine;
            var text1Length = text1.Length;
            var text2PadLeftLength = lineNumChars - text1Length;
            newText += text1;
            newText += text2.PadLeft(text2PadLeftLength, ' ');
            return newText;
        }

        static string ReplaceSubstringFromIndex(string original, int startIndex, string newSubstring)
        {
            // Validate the starting index
            if (startIndex < 0 || startIndex > original.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Starting index is out of range.");
            }

            // Get the part of the string before the starting index
            string prefix = original.Substring(0, startIndex);

            // Get the part of the string after the starting index
            string suffix = original.Length > startIndex ? original.Substring(startIndex) : "";

            // Combine prefix, new substring, and suffix
            return prefix + newSubstring + suffix;
        }

        static string TruncateString(string original, int maxLength)
        {
            // Validate the max length
            if (maxLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Maximum length cannot be negative.");
            }

            // Return the original string if it's already within the maximum length
            if (original.Length <= maxLength)
            {
                return original;
            }

            // Truncate the string
            return original.Substring(0, maxLength);
        }

        private string ItemFirstLine(int quantity, string unit, string itemCode, decimal price, decimal total, int lineNumChars = 40)
        {
            var newText = new string(' ', lineNumChars);
            newText = ReplaceSubstringFromIndex(newText, 0, quantity.ToString());
            newText = ReplaceSubstringFromIndex(newText, 4, unit);
            newText = ReplaceSubstringFromIndex(newText, 7, itemCode);
            newText = ReplaceSubstringFromIndex(newText, lineNumChars - 11 - price.ToString("N2").Length, price.ToString("N2"));
            newText = ReplaceSubstringFromIndex(newText, lineNumChars - total.ToString("N2").Length, total.ToString("N2"));
            return Environment.NewLine + newText.Trim();
        }

        private string ItemSecondLine(string description)
        {
            var newText = new string(' ', 4) + TruncateString(description, 36);
            return Environment.NewLine + newText.TrimEnd();
        }

        public string FormatReceiptContent(List<Purchase> purchases, string cashier, 
            decimal cashTender, int salesInvoice, string paymentMode, string customer = "")
        {
            var receipt = Environment.NewLine;
            var customerName = customer;
            receipt += Environment.NewLine; 
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += CenterLine("MYSAVERS PHARMACY");
            receipt += CenterLine("D. MACAPAGAL HIWAY SANGI TOLEDO CITY CEBU");
            receipt += CenterLine("PROPRIETOR: VIRGILENE A. ALICAYA");
            receipt += CenterLine("VAT REG TIN: 416-473-778-00002");
            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += SingleLine("Customer Name: " + customerName);
            receipt += SingleLine("Customer Address:");
            receipt += SingleLine("Customer TIN:");
            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += JustifyTwoTexts("SALES INVOICE NO", salesInvoice.ToString().PadLeft(10, '0')); 
            receipt += SingleLine("ENCODER " + cashier); 
            receipt += DashDivider(Convert.ToInt16(_posDashCount));

            salesInvoice += 10;
            //loop
            foreach (var purchase in purchases)
            {
                receipt += ItemFirstLine(purchase.Quantity, "PC", purchase.ItemCode, purchase.Price, purchase.Total);
                receipt += ItemSecondLine(purchase.Description);
            }
            //end loop

            var lineItems = purchases.Count;
            var quantity = purchases.Sum(x => x.Quantity);
            var grossSales = purchases.Sum(x => x.Total);
            var totalDiscount = purchases.Sum(x => x.DiscountAmount);
            var totalDue = grossSales - totalDiscount;
            var lessReturns = 0.00M;
            var totalVat = purchases.Sum(x => x.VatAmount);
            var vattableSales = totalDue - totalVat;
            var cashTenderOutput = cashTender.ToString("N2");
            var changeOutput = (cashTender - totalDue).ToString("N2");
            if (paymentMode == "GCASH" || paymentMode == "MAYA" || paymentMode == "CARD")
            {
                cashTenderOutput = paymentMode;
                changeOutput = "0.00";
            }

            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += SingleLine("Line Items: " + lineItems + "  Quantity: " + quantity + ".000000");
            receipt += SingleLine("TOTAL DUE                                    " + totalDue.ToString("N2"));
            receipt += SingleLine("CASH                                              " + cashTenderOutput);
            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += SingleLine("CHANGE                                           " + changeOutput);
            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += SingleLine("GROSS SALES                              " + grossSales.ToString("N2"));
            receipt += SingleLine("LESS DISCOUNT                              " + totalDiscount.ToString("N2"));
            receipt += SingleLine("LESS RETURNS                                " + lessReturns.ToString("N2"));
            receipt += Environment.NewLine;
            receipt += SingleLine("VAT 12%                                         " + totalVat.ToString("N2"));
            receipt += SingleLine("VATTABLE SALES                       " + vattableSales.ToString("N2"));
            receipt += SingleLine("VAT EXEMPT SALES                      " + 0.00M.ToString("N2"));
            receipt += SingleLine("ZERO RATED SALES                      " + 0.00M.ToString("N2"));
            receipt += Environment.NewLine;
            receipt += SingleLine("SALES INVOICE NO                " + salesInvoice.ToString().PadLeft(10, '0'));
            receipt += SingleLine("SO NO                                       " + (salesInvoice-1).ToString().PadLeft(10, '0'));
            receipt += SingleLine("MSN                                       " + "PH230100842");
            receipt += SingleLine("CASHIER: " + cashier);
            receipt += JustifyTwoTexts("DATE/TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += Environment.NewLine;
            receipt += SingleLine("Accredited POS Supplier Trade Name");
            receipt += SingleLine(" Live Help 4 Us Computer Services");
            receipt += SingleLine(" Bantayan Dumaguete City");
            receipt += SingleLine(" TIN 935546208");
            receipt += JustifyTwoTexts("ACCREG NO.", "07993554620800057553501");
            receipt += SingleLine("ACCREG DATE ISSUED May 24, 2012");
            receipt += SingleLine("EFFECTIVITY DATE August 27, 2027");
            receipt += JustifyTwoTexts("PERMIT NO.", "FP082024-083-0463936-00002");
            receipt += JustifyTwoTexts("MIN    NO.", "24082213000273970");
            receipt += DashDivider(Convert.ToInt16(_posDashCount));
            receipt += Environment.NewLine;
            receipt += CenterLine("THIS SERVES AS YOUR SALES INVOICE", 34);
            receipt += Environment.NewLine;
            receipt += CenterLine("THANK YOU COME AGAIN!", 40);
            receipt += CenterLine("SAVE SALES INVOICE FOR RETURNS", 34);
            receipt += CenterLine("  AND EXCHANGES", 42);
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            receipt += Environment.NewLine;
            return receipt;
        }
    }
}

