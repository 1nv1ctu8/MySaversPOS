using PosSystem.Model;
using PosSystem.Repository;
using System;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmItemManagement : Form
    {
        string stitle = "Item Management";
        readonly PosRepository _posRepository = new PosRepository();
        frmPOS _fPos;
        int _currentQuantity;

        public frmItemManagement(frmPOS frmpos)
        {
            InitializeComponent();
            //cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;

            panelRadio.Controls.Add(radioEntry);
            panelRadio.Controls.Add(radioUpdate);
            panelRadio.Controls.Add(radioDelete);
            panelRadio.UseCustomBackColor = true;
            panelRadio.BackColor = System.Drawing.Color.WhiteSmoke;

            radioEntry.UseCustomBackColor = true;
            radioEntry.BackColor = System.Drawing.Color.WhiteSmoke;
            radioUpdate.UseCustomBackColor = true;
            radioUpdate.BackColor = System.Drawing.Color.WhiteSmoke;
            radioDelete.UseCustomBackColor = true;
            radioDelete.BackColor = System.Drawing.Color.WhiteSmoke;
            radioEntry.Checked = true;

            lblEntry.Visible = true;

            _fPos = frmpos;

            foreach (Control c in numericUpDownQtyInput.Controls)
            {
                if (c is TextBox textBox)
                {
                    textBox.TextChanged += TextBox_TextChanged;
                    break;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemCode.Text))
            {
                MessageBox.Show("Please provide an Item Code.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtItemCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please provide an Item Description.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescription.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaterialCost.Text))
            {
                MessageBox.Show("Please provide the Material Cost.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMaterialCost.Focus();
                return;
            }

            if (!decimal.TryParse(txtMaterialCost.Text, out _) && !int.TryParse(txtMaterialCost.Text, out _))
            {
                MessageBox.Show("Please enter a numeric value for Material Cost.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrice.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Please provide the Unit.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUnit.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please provide the Price.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrice.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out _) && !int.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Please enter a numeric value for Price.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrice.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                MessageBox.Show("Please provide the Category.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbVattable.Text))
            {
                MessageBox.Show("Please indicate if Vattable or not.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbVattable.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Please provide Quantity.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQuantity.Focus();
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out _))
            {
                MessageBox.Show("Please enter a numeric value for Quantity.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQuantity.Focus();
                return;
            }


            Item item = new Item
            {
                ItemCode = txtItemCode.Text,
                Description = txtDescription.Text,
                MaterialCost = Convert.ToDecimal(txtMaterialCost.Text),
                Units = txtUnit.Text,
                Retail = Convert.ToDecimal(txtPrice.Text),
                CategoryName = txtCategory.Text,
                TaxTypeId = cmbVattable.Text.ToLower() == "yes" ? 1 : 0,
                InventoryQuantity = Convert.ToInt16(txtQuantity.Text)
            };

            ItemEntryHistory itemEntryHistory = new ItemEntryHistory
            {
                ItemCode = item.ItemCode,
                MaterialCost = item.MaterialCost,
                Price = item.Retail,
                Quantity = item.InventoryQuantity,
                UpdatedBy = _fPos._user.Id,
                Remarks = txtRemarks.Text
            };

            int result = 0;

            try
            {
                var itemCheck = _posRepository.GetItemByItemCodeItemManagement(txtItemCode.Text.Trim());

                if (radioEntry.Checked)
                {
                    if (itemCheck != null && !string.IsNullOrWhiteSpace(item.ItemCode))
                    {
                        MessageBox.Show("Item cannot be added.\n " +
                            "Item with ItemCode " + itemCheck.ItemCode + " already exists.\n " +
                            "You can Update Item instead.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        item.InventorySold = 0;
                        result = _posRepository.InsertItem(item);
                        _posRepository.InsertItemEntryHistory(itemEntryHistory);
                    }
                }
                else if (radioUpdate.Checked)
                {
                    if (item.InventoryQuantity < itemCheck.InventoryQuantity && _fPos._user.Role != 1)
                    {
                        MessageBox.Show("Item quantity cannot be reduced.\n " +
                                            "Approval from admin needed.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        var oldItemValues = _posRepository.GetItemByItemCodeItemManagement(txtItemCode.Text.Trim());
                        var updatedItemEntryHistory = GetNewItemHistoryValues(oldItemValues, itemEntryHistory);

                        result = _posRepository.UpdateItem(item);
                        
                        _posRepository.InsertItemEntryHistory(updatedItemEntryHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (result == 1)
            {
                MessageBox.Show("Item " + (radioEntry.Checked ? "Entry" : "Update") + " Successful!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                if (radioUpdate.Checked)
                {
                    EnableControls(false);
                }
                return;
            }
            else
            {
                MessageBox.Show((radioEntry.Checked ? "Entry" : "Update") + " Failed! Please try again.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void radioEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEntry.Checked)
            {
                lblEntry.Visible = true;
                lblUpdate.Visible = false;
                lblDelete.Visible = false;
                btnLoadItem.Visible = false;
                txtItemCode.Enabled = true;
                txtItemCode.Text = string.Empty;
                txtDescription.Enabled = true;
                txtDescription.Text = string.Empty;
                txtMaterialCost.Enabled = true;
                txtMaterialCost.Text = string.Empty;
                txtUnit.Enabled = true;
                txtUnit.Text = string.Empty;
                txtPrice.Enabled = true;
                txtPrice.Text = string.Empty;
                txtCategory.Enabled = true;
                txtCategory.Text = string.Empty;
                cmbVattable.Enabled = true;
                cmbVattable.Text = string.Empty;
                txtQuantity.Enabled = true;
                txtQuantity.Text = string.Empty;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
                btnDelete.Visible = false;
                numericUpDownQtyInput.Visible = false;
                txtRemarks.Enabled = true;
                txtRemarks.Text = string.Empty;

                ReadOnlyControls(false);
            }
        }

        private void radioUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUpdate.Checked)
            {                
                lblUpdate.Visible = true;
                lblEntry.Visible = false;
                lblDelete.Visible = false;
                btnLoadItem.Visible = true;
                txtItemCode.Enabled = true;
                txtItemCode.Text = string.Empty;
                txtDescription.Enabled = false;
                txtDescription.Text = string.Empty;
                txtMaterialCost.Enabled = false;
                txtMaterialCost.Text = string.Empty;
                txtUnit.Enabled = false;
                txtUnit.Text = string.Empty;
                txtPrice.Enabled = false;
                txtPrice.Text = string.Empty;
                txtCategory.Enabled = false;
                txtCategory.Text = string.Empty;
                txtQuantity.Enabled = false;
                txtQuantity.Text = string.Empty;
                cmbVattable.Enabled = false;
                cmbVattable.Text = string.Empty;
                btnSubmit.Visible = true;
                btnDelete.Visible = false;
                btnSubmit.Enabled = false;
                numericUpDownQtyInput.Visible = true;
                numericUpDownQtyInput.Enabled = false;
                txtRemarks.Enabled = false;
                txtRemarks.Text = string.Empty;

                ReadOnlyControls(false);

                cmbVattable.Enabled = false;
            }
        }

        private void radioDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDelete.Checked)
            {
                lblDelete.Visible = true;
                lblUpdate.Visible = false;
                lblEntry.Visible = false;
                btnLoadItem.Visible = true;
                txtItemCode.Enabled = true;
                txtItemCode.Text = string.Empty;
                txtDescription.Enabled = false;
                txtDescription.Text = string.Empty;
                txtMaterialCost.Enabled = false;
                txtMaterialCost.Text = string.Empty;
                txtUnit.Enabled = false;
                txtUnit.Text = string.Empty;
                txtPrice.Enabled = false;
                txtPrice.Text = string.Empty;
                txtCategory.Enabled = false;
                txtCategory.Text = string.Empty;
                txtQuantity.Enabled = false;
                txtQuantity.Text = string.Empty;
                cmbVattable.Enabled = false;
                cmbVattable.Text = string.Empty;
                btnSubmit.Visible = false;
                btnDelete.Visible = true;
                btnDelete.Enabled = false;
                numericUpDownQtyInput.Visible = false;
                txtRemarks.Visible = false;
                txtRemarks.Text = string.Empty;

                ReadOnlyControls(true);
            }
        }

        private void btnLoadItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemCode.Text))
            {
                MessageBox.Show("Please provide an Item Code.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtItemCode.Focus();
                return;
            }

            var item = _posRepository.GetItemByItemCodeItemManagement(txtItemCode.Text.Trim());

            if (item == null || string.IsNullOrWhiteSpace(item.ItemCode)) 
            {
                MessageBox.Show("Item with ItemCode " + txtItemCode.Text + " not found.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;           
            }

            if (item != null && !string.IsNullOrWhiteSpace(item.ItemCode))
            {
                EnableControls();

                txtItemCode.Text = item.ItemCode;
                txtDescription.Text = item.Description;
                txtMaterialCost.Text = item.MaterialCost.ToString("#,##0.00");
                txtUnit.Text = item.Units;
                txtPrice.Text = item.Retail.ToString("#,##0.00");
                txtCategory.Text = item.CategoryName;
                txtQuantity.Text = item.InventoryQuantity.ToString();
                cmbVattable.Text = item.TaxTypeId == 1 ? "Yes" : "No";
                btnDelete.Enabled = true;
                _currentQuantity = item.InventoryQuantity;
                txtQuantity.ReadOnly = true;
                numericUpDownQtyInput.Enabled = true;

                textItemCodeCommon.Text = item.ItemCode;
            }
        }

        private void ClearFields()
        {
            //txtItemCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtMaterialCost.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            cmbVattable.Text = string.Empty;
            numericUpDownQtyInput.Value = 0;
            txtRemarks.Text = string.Empty;
        }

        private void EnableControls(bool enable = true)
        {
            txtDescription.Enabled = enable;
            txtMaterialCost.Enabled = enable;
            txtUnit.Enabled = enable;
            txtPrice.Enabled = enable;
            txtCategory.Enabled = enable;
            txtQuantity.Enabled = enable;
            cmbVattable.Enabled = enable;
            btnSubmit.Enabled = enable;
            numericUpDownQtyInput.Enabled = enable;
            txtRemarks.Enabled = enable;
        }

        private void ReadOnlyControls(bool readOnly = true)
        {        
            txtDescription.ReadOnly = readOnly;
            txtMaterialCost.ReadOnly = readOnly;
            txtUnit.ReadOnly = readOnly;
            txtPrice.ReadOnly = readOnly;
            txtCategory.ReadOnly = readOnly;
            txtQuantity.ReadOnly = readOnly;
            if (readOnly)
            {
                cmbVattable.DropDownStyle = ComboBoxStyle.Simple;
                cmbVattable.Enabled = false;
            }
            else
            {
                cmbVattable.DropDownStyle = ComboBoxStyle.DropDown;
                cmbVattable.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to delete Item with ItemCode {txtItemCode.Text}?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(txtItemCode.Text))
                {
                    result = _posRepository.DeleteItem(txtItemCode.Text);
                }

                if (result == 1)
                {
                    MessageBox.Show($"Item with ItemCode {txtItemCode.Text} has been Deleted!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Delete Failed! Please try again.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                return;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControlItems.SelectedIndex;
            if (index == 1)
            {
                dataGridViewItemHistory.Rows.Clear();

                if (!string.IsNullOrWhiteSpace(textItemCodeCommon.Text))
                {
                    textItemCodeUH.Text = textItemCodeCommon.Text.Trim();
                }

                var itemCode = textItemCodeUH.Text.Trim();

                if (!string.IsNullOrWhiteSpace(itemCode))
                {
                    dtDateFrom.Value = _posRepository.GetEarliestItemEntry(itemCode);

                    var item = _posRepository.GetItemByItemCodeItemManagement(itemCode);

                    if (item != null)
                    {
                        labelItemCodeValue.Text = itemCode;

                        labelDescriptionValue.Text = item.Description;

                        var quantity = item.InventoryQuantity;

                        labelCostValueValue.Text = (item.MaterialCost * quantity).ToString("#,##0.00");
                        labelSaleValueValue.Text = (item.Retail * quantity).ToString("#,##0.00");

                        labelHistoryQuantityValue.Text = item.InventoryQuantity.ToString();
                        labelItemsSoldValue.Text = item.InventorySold.ToString();
                    }
                }
            }

            else if (index == 2)
            {
                dataGridViewSalesHistory.Rows.Clear();  

                if (!string.IsNullOrWhiteSpace(textItemCodeCommon.Text))
                {
                    textItemCodeSH.Text = textItemCodeCommon.Text.Trim();
                }

                var itemCode = textItemCodeSH.Text.Trim();

                if (!string.IsNullOrWhiteSpace(itemCode))
                {
                    var item = _posRepository.GetItemByItemCodeItemManagement(itemCode);

                    if (item != null)
                    {
                        labelItemCodeValueSH.Text = itemCode;

                        labelDescValueSH.Text = item.Description;
                    }
                }
            }

            dataGridViewItemHistory.Rows.Clear();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dateFrom = dtDateFrom.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            var dateTo = dtDateTo.Value.ToString("yyyy-MM-dd") + " 23:59:59";

            if (!string.IsNullOrWhiteSpace(textItemCodeCommon.Text))
            {
                if (!string.IsNullOrWhiteSpace(textItemCodeUH.Text)
                    && (textItemCodeUH.Text == textItemCodeCommon.Text))
                textItemCodeUH.Text = textItemCodeCommon.Text.Trim();
            }

            var itemCode = textItemCodeUH.Text.Trim();

            dataGridViewItemHistory.Rows.Clear();

            if (!string.IsNullOrWhiteSpace(itemCode))
            {
                var itemHistory = _posRepository.GetItemEntryHistoryByItemCode(itemCode, dateFrom, dateTo);

                if (itemHistory.Count > 0)
                {
                    int i = 0;

                    dataGridViewItemHistory.Columns["cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewItemHistory.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewItemHistory.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewItemHistory.Rows.Clear();

                    foreach (var item in itemHistory)
                    {
                        i++;
                        dataGridViewItemHistory.Rows.Add(i, item.DateTime, item.MaterialCost.ToString("#,##0.00"), item.Price.ToString("#,##0.00"), item.Quantity.ToString("F0"), item.UserName);
                    }

                    textItemCodeCommon.Text = itemCode;
                }
            }
        }

        private void numericUpDownQtyInput_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownQtyInputChange();
        }

        private void numericUpDownQtyInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                AddQuantityOne();
            }

            else if (e.KeyCode == Keys.Down)
            {
                DeductQuantityOne();
            }
        }

        private void AddQuantityOne()
        {
            txtQuantity.Text = (_currentQuantity + 1).ToString();
            numericUpDownQtyInput.Value++;
        }

        private void DeductQuantityOne()
        {
            txtQuantity.Text = (_currentQuantity - 1).ToString();
            numericUpDownQtyInput.Value--;
        }

        private void numericUpDownQtyInputChange()
        {
            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                txtQuantity.Text = "0";
            }

            txtQuantity.Text = (_currentQuantity + Convert.ToInt32(numericUpDownQtyInput.Value.ToString())).ToString();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Optional: validate input
            if (decimal.TryParse(((TextBox)sender).Text, out decimal result))
            {
                txtQuantity.Text = (_currentQuantity + result).ToString();
            }
            else
            {
                txtQuantity.Text = _currentQuantity.ToString();
            }
        }

        private ItemEntryHistory GetNewItemHistoryValues(Item oldItemValues, ItemEntryHistory itemEntryHistory)
        {
            var itemHistory = new ItemEntryHistory();

            if (oldItemValues != null) 
            {
                itemHistory = new ItemEntryHistory
                {
                    ItemCode = itemEntryHistory.ItemCode,
                    MaterialCost = itemEntryHistory.MaterialCost - oldItemValues.MaterialCost,
                    Price = itemEntryHistory.Price - oldItemValues.Retail,
                    Quantity = itemEntryHistory.Quantity - oldItemValues.InventoryQuantity,
                    UpdatedBy = itemEntryHistory.UpdatedBy,               
                    Remarks = itemEntryHistory.Remarks
                };
            }

            return itemHistory;
        }

        private void BtnLoadSH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textItemCodeCommon.Text))
            {
                if (!string.IsNullOrWhiteSpace(textItemCodeSH.Text)
                    &&(textItemCodeSH.Text == textItemCodeCommon.Text))
                textItemCodeSH.Text = textItemCodeCommon.Text.Trim();
            }

            var itemCode = textItemCodeSH.Text.Trim();

            dataGridViewSalesHistory.Rows.Clear();

            if (!string.IsNullOrWhiteSpace(itemCode))
            {
                var thisItem = _posRepository.GetItemByItemCodeItemManagement(itemCode);

                if (thisItem != null)
                {
                    labelItemCodeValueSH.Text = itemCode;

                    labelDescValueSH.Text = thisItem.Description;
                }

                var itemSalesUsers = _posRepository.GetItemSalesUsersByItemCode(itemCode);

                textItemCodeCommon.Text = itemCode;

                if (itemSalesUsers.Count > 0)
                {
                    int i = 0;

                    dataGridViewSalesHistory.Columns["dgDateSH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewSalesHistory.Columns["dgTransNumSH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewSalesHistory.Columns["dgQuantitySH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewSalesHistory.Columns["dgCashierSH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewSalesHistory.Rows.Clear();

                    foreach (var item in itemSalesUsers)
                    {
                        i++;
                        dataGridViewSalesHistory.Rows.Add(i, item.SaleDate, item.TransNum, item.Quantity, item.Cashier);
                    }
                }
            }
        }
    }
}
