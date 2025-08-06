using PosSystem.Logic;
using PosSystem.Model;
using PosSystem.Repository;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmTender : Form
    {

        //SqlConnection cn = new SqlConnection();
        //SqlCommand cm = new SqlCommand();
        //SqlDataReader dr;
        //DBConnection dbcon = new DBConnection();
        frmPOS _fPos;
        //frmStoreSetting fstore;

        readonly PosRepository _posRepository = new PosRepository();
        readonly PosLogic _posLogic = new PosLogic();
        const int _lineNumChars = 40;

        public frmTender(frmPOS fp)
        {
            InitializeComponent();
            _fPos = fp;
            _fPos._salesSaved = false;

            //cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = double.Parse(txtSale.Text);
                double cash = double.Parse(txtCash.Text.ToString());               
                double change = cash - sale;

                txtChange.Text = change.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            object selectedItem = cmbPaymentMode.SelectedItem;
            string selectedText = selectedItem.ToString().Trim();

            if (selectedText == "CASH")
            {
                try
                {
                    if ((double.Parse(txtChange.Text) < 0) || (txtChange.Text == String.Empty))
                    {
                        MessageBox.Show("Insufficient Amount Tendered. Please enter the corret amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //for (int i = 0; i < _fPos.dataGridView1.Rows.Count; i++)
                        //{
                        //cn.Open();
                        //cm = new SqlCommand("update TblProduct1 set qty = qty - " + int.Parse(fpos.dataGridView1.Rows[i].Cells[5].Value.ToString()) + " where pcode = '" + fpos.dataGridView1.Rows[i].Cells[2].Value.ToString() + "'", cn);
                        //cm.ExecuteNonQuery();
                        //cn.Close();

                        //var id = Convert.ToInt32(_fPos.dataGridView1.Rows[i].Cells[1].Value);
                        //_posRepository.UpdateCartStatus(id, 2);                       
                        //cn.Open();
                        //cm = new SqlCommand("update tblCart1 set status = 'Sold' where id = '" + fpos.dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", cn);
                        //cm.ExecuteNonQuery();
                        //cn.Close();
                        //}
                        //frmResipt frm = new frmResipt(fpos);
                        //frm.LoadReport(txtCash.Text, txtChange.Text);
                        //frm.ShowDialog();

                        var transNum = InsertSales();
                        double sale = double.Parse(txtSale.Text);
                        double cash = double.Parse(txtCash.Text.ToString());
                        var cashDecimal = Convert.ToDecimal(cash);
                        _fPos._cashTender = cashDecimal;
                        _posRepository.InsertCashTender(transNum, cashDecimal);
                        double change = cash - sale;
                        _fPos._change = Convert.ToDecimal(change);
                        _fPos._salesSaved = true;
                        _fPos._paymentMode = selectedText;
                        MessageBox.Show("Payment successfully saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Insufficient amount.Please enter the correct amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (selectedText == "GCASH" || selectedText == "MAYA")
            {
                var cellNum = textBoxNonCash1.Text.Trim();

                if (string.IsNullOrWhiteSpace(cellNum))
                {
                    MessageBox.Show("Please enter Cell Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cellNum.Length != 11)
                {
                    MessageBox.Show("Cell Number should be 11 digits long, starting with 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBoxNonCash2.Text.Trim()))
                {
                    MessageBox.Show("Please enter Reference Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var transNum = InsertSales();

                    if (selectedText == "GCASH")
                    {
                        _posRepository.InsertSalesGCash(transNum, textBoxNonCash1.Text, textBoxNonCash2.Text);                        
                    }

                    if (selectedText == "MAYA")
                    {
                        _posRepository.InsertSalesMaya(transNum, textBoxNonCash1.Text, textBoxNonCash2.Text);
                    }

                    _fPos._salesSaved = true;
                    _fPos._paymentMode = selectedText;
                    Dispose();
                }
            }

            if (selectedText == "CARD")
            {
                var cardTypeIndex = cmbCardType.SelectedIndex;

                if (cardTypeIndex == -1)
                {
                    MessageBox.Show("Please enter Card Type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBoxNonCash2.Text.Trim()))
                {
                    MessageBox.Show("Please enter Last 4 digit of Card.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textBoxNonCash2.Text.Trim().Length != 4)
                {
                    MessageBox.Show("Last 4 digit of Card should be 4 digits long.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var transNum = InsertSales();
                    _posRepository.InsertSalesCard(transNum, cmbCardType.SelectedItem.ToString(), textBoxNonCash2.Text);
                    _fPos._salesSaved = true;
                    _fPos._paymentMode = selectedText;
                    Dispose();
                }
            }

        }

        private void frmSettel_Load(object sender, EventArgs e)
        {
            cmbPaymentMode.SelectedIndex = 0;
            cmbCardType.SelectedIndex = -1;
            txtCash.Focus();
            ShowCashControls(true);

        }

        private void txtSale_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmTender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();

            }
            
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(sender, e);
            }

            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3 || e.KeyCode == Keys.D4 ||
                e.KeyCode == Keys.D5 || e.KeyCode == Keys.D6 || e.KeyCode == Keys.D7 || e.KeyCode == Keys.D8 || e.KeyCode == Keys.D9)
            {
                txtCash.Focus();
            }
        }

        private void frmTender_Shown(object sender, EventArgs e)
        {
            txtCash.Focus();
        }

        private string InsertSales()
        {
            var vatPercent = _fPos._vat;
            var transNum = _fPos.lblTransno.Text.Trim();
            var purchases = _posLogic.GetPurchaseInfoByTransNumAndStatus(transNum, vatPercent, 1);

            foreach (var purchase in purchases) 
            {
                Sales sales = new Sales
                {
                    TransNum = purchase.TransNum,
                    ItemCode = purchase.ItemCode,
                    Description = purchase.Description,
                    Price = purchase.Price,
                    Quantity = purchase.Quantity,
                    VatPercent = purchase.VatPercent,
                    VatAmount = purchase.VatAmount,
                    DiscountPercent = purchase.DiscountPercent,
                    DiscountAmount = purchase.DiscountAmount,
                    DiscountDescription = purchase.DiscountDesc,
                    Total = purchase.Total,
                    Cashier = purchase.CashierFName
                };

                _posRepository.InsertSales(sales);

                _posRepository.UpdateItemInventory(sales.ItemCode, sales.Quantity);

                _posRepository.UpdateCartStatus(purchase.CartId, 2);
            }

            return purchases.First().TransNum;
        }

        private string CenterLine(string text)
        {
            var textCount = text.Trim().Length;
            var lineLength = (_lineNumChars / 2) + (textCount / 2);
            return text.PadLeft(lineLength, ' ');
        }

        private string DashDivider(int times)
        {
            return new string('-', times);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedItem = cmbPaymentMode.SelectedItem;
            string selectedText = selectedItem.ToString().Trim();
            if (selectedText == "CASH")
            {
                txtCash.Focus();
                ShowCashControls(true);
            }

            if (selectedText == "GCASH" || selectedText == "MAYA")
            {
                ShowCashControls(false);
                labelNonCash1.Text = "Cell Number";
                labelNonCash2.Text = "Reference Number";
                textBoxNonCash1.Visible = true;
                textBoxNonCash1.Text = "";
                cmbCardType.Visible = false;
            }

            if (selectedText == "CARD")
            {
                ShowCashControls(false);
                labelNonCash1.Text = "Card Type";
                labelNonCash2.Text = "Last 4 Digit";
                textBoxNonCash1.Text = "";
                textBoxNonCash1.Visible = false;
                cmbCardType.Visible = true;
                textBoxNonCash1.Text = "";
            }
        }

        private void ShowCashControls(bool show)
        {
            labelCashTender.Visible = show;
            txtCash.Visible = show;
            labelChange.Visible = show;
            txtChange.Visible = show;

            btn00.Visible = show;
            btn0.Visible = show;
            btn1.Visible = show;
            btn2.Visible = show;
            btn3.Visible = show;
            btn4.Visible = show;
            btn5.Visible = show;
            btn6.Visible = show;
            btn7.Visible = show;
            btn8.Visible = show;
            btn9.Visible = show;
            btnC.Visible = show;

            labelNonCash1.Visible = !show;
            textBoxNonCash1.Visible = !show;
            labelNonCash2.Visible = !show;
            textBoxNonCash2.Visible = !show;
            cmbCardType.Visible = !show;

            if (show)
            {
                txtCash.Focus();
                this.Size = new System.Drawing.Size(300, 530);
            }
            else 
            {
                this.Size = new System.Drawing.Size(300, 350);
            }
        }
    }
}
