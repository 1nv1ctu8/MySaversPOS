using System;
using System.Drawing;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmMockReceipt : Form
    {
        const int _lineNumChars = 40;
        TextBox textBox;

        public frmMockReceipt()
        {
            InitializeComponent();
            textBox = new TextBox();
            textBox.Multiline = true; // Allow multiple lines
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Width = 350;
            textBox.Height = 600;

            // Set the font to a monospaced font (e.g., Consolas)
            textBox.Font = new Font("Courier New", 8); // Ensure the font name is correct

            // Add the TextBox to the form
            Controls.Add(textBox);
        }


        private string CenterLine(string text)
        {
            var textCount = text.Trim().Length;
            var newText = Environment.NewLine;
            var lineToStart = (_lineNumChars / 2) - (textCount / 2);
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

        private string JustifyTwoTexts(string text1, string text2)
        {
            var newText = Environment.NewLine;
            var text1Length = text1.Length;
            var text2PadLeftLength = _lineNumChars - text1Length;
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

        private string ItemFirstLine(int quantity, string unit, string itemCode, decimal price, decimal total)
        {
            var newText = new string(' ', _lineNumChars);
            newText = ReplaceSubstringFromIndex(newText, 0, quantity.ToString());
            newText = ReplaceSubstringFromIndex(newText, 4, unit);
            newText = ReplaceSubstringFromIndex(newText, 7, itemCode);
            newText = ReplaceSubstringFromIndex(newText, _lineNumChars-11-price.ToString("N2").Length, price.ToString("N2"));
            newText = ReplaceSubstringFromIndex(newText, _lineNumChars-total.ToString("N2").Length, total.ToString("N2"));
            return Environment.NewLine + newText.Trim();
        }

        private string ItemSecondLine(string description)
        {
            var newText = new string(' ', 4) + TruncateString(description, 36);
            return Environment.NewLine + newText.TrimEnd();
        }

        private void frmMockReceipt_Load(object sender, EventArgs e)
        {           
            //textBox = new TextBox();
            //textBox.Multiline = true; // Allow multiple lines
            //textBox.ScrollBars = ScrollBars.Vertical;
            //textBox.Width = 350;
            //textBox.Height = 600;

            //// Set the font to a monospaced font (e.g., Consolas)
            //textBox.Font = new Font("Courier New", 8); // Ensure the font name is correct

            //// Add the TextBox to the form
            //Controls.Add(textBox);
            //var encoder = "Owen";
            //var quantity = 121;
            //var receipt = Environment.NewLine;
            //receipt += CenterLine("MYSAVERS PHARMACY");
            //receipt += CenterLine("D. MACAPAGAL HIWAY SANGI TOLEDO CITY CEBU");
            //receipt += CenterLine("PROPRIETOR: VIRGILENE A. ALICAYA");
            //receipt += CenterLine("VAT REG TIN: 416-473-778-00002");
            //receipt += DashDivider(_lineNumChars);
            //receipt += SingleLine("Customer Name:");
            //receipt += SingleLine("Customer Address:");
            //receipt += SingleLine("Customer TIN:");
            //receipt += DashDivider(_lineNumChars);
            //receipt += JustifyTwoTexts("SALES INVOICE NO", "00000000155"); //with variable
            //receipt += SingleLine("ENCODER " + encoder); //with variable
            //receipt += DashDivider(_lineNumChars);
            ////loop
            //receipt += ItemFirstLine(10, "PC", "4800086049674", 121.00M, 1210.00M);
            //receipt += ItemSecondLine("BEST FOODS RGLR MYNS MAYOMAGIC CDOY 24X220");
            //receipt += ItemFirstLine(2, "PC", "4800086049674", 600.00M, 1200.00M);
            //receipt += ItemSecondLine("Broncho Rub Citrus 10gm");
            //receipt += ItemFirstLine(10, "PC", "4800086044", 120.00M, 1200.00M);
            //receipt += ItemSecondLine("BACTIDOL LIQUID 120ML 60ML HEXETIDINE");
            //receipt += ItemFirstLine(100, "PC", "48000860444", 150.00M, 15000.00M);
            //receipt += ItemSecondLine("CALCI-AID 500MG CAP 100S");
            //receipt += ItemFirstLine(1, "PC", "4800086049674", 80.00M, 80.00M);
            //receipt += ItemSecondLine("BUY 1 TIE GREAT TASTE CHOCO TWIN GET 1PC GREAT TASTE CHOCO FREE");
            ////end loop
            //receipt += DashDivider(_lineNumChars);
            //receipt += SingleLine("Line Items: " + quantity + "  Quantity: " + quantity + ".000000");
            //receipt += JustifyTwoTexts("TOTAL DUE", 18690.00M.ToString("N2"));
            //receipt += JustifyTwoTexts("CASH", 19000.00M.ToString("N2"));
            //receipt += DashDivider(_lineNumChars);
            //receipt += JustifyTwoTexts("CHANGE", 310.00M.ToString("N2"));
            //receipt += DashDivider(_lineNumChars);
            //receipt += JustifyTwoTexts("GROSS SALES", 18690.00M.ToString("N2"));
            //receipt += JustifyTwoTexts("LESS DISCOUNT", 0.00M.ToString("N2"));
            //receipt += JustifyTwoTexts("LESS RETURNS", 0.00M.ToString("N2"));
            //receipt += Environment.NewLine;
            //receipt += JustifyTwoTexts("VAT 12%", 2242.80M.ToString("N2"));
            //receipt += JustifyTwoTexts("VATTABLE SALES", 16447.2M.ToString("N2"));
            //receipt += JustifyTwoTexts("VAT EXEMPT SALES", 0.00M.ToString("N2"));
            //receipt += JustifyTwoTexts("ZERO RATED SALES", 0.00M.ToString("N2"));
            //receipt += Environment.NewLine;
            //receipt += JustifyTwoTexts("SALES INVOICE NO", "0000000155");
            //receipt += JustifyTwoTexts("SO NO", "0000000154");
            //receipt += JustifyTwoTexts("MSN", "PH230100842");
            //receipt += SingleLine("CASHIER: OWEN LAGULA");
            //receipt += JustifyTwoTexts("DATE/TIME", "2024-09-09 23:46:59");
            //receipt += DashDivider(_lineNumChars);
            //receipt += Environment.NewLine;
            //receipt += SingleLine("Accredited POS Supplier Trade Name");
            //receipt += SingleLine(" Live Help 4 Us Computer Services");
            //receipt += SingleLine(" Bantayan Dumaguete City");
            //receipt += SingleLine(" TIN 935546208");
            //receipt += JustifyTwoTexts("ACCREG NO.", "07993554620800057553501");
            //receipt += SingleLine("ACCREG DATE ISSUED May 24, 2012");
            //receipt += SingleLine("EFFECTIVITY DATE August 27, 2024");
            //receipt += JustifyTwoTexts("PERMIT NO.", "FP082024-083-0463936-00002");
            //receipt += JustifyTwoTexts("MIN    NO.", "24082213000273970");
            //receipt += DashDivider(_lineNumChars);
            //receipt += Environment.NewLine;
            //receipt += CenterLine("THIS SERVES AS YOUR SALES INVOICE");
            //receipt += Environment.NewLine;
            //receipt += CenterLine("THANK YOU COME AGAIN!");
            //receipt += CenterLine("SAVE SALES INVOICE FOR RETURNS");
            //receipt += CenterLine("AND EXCHANGES");
            //receipt += Environment.NewLine;
            //receipt += Environment.NewLine;
            //receipt += Environment.NewLine;
            //textBox.Text = receipt;
        }

        public void ShowReceipt(string receiptContent)
        {
            textBox.Text = receiptContent;  
        }
    }
}

