using PosSystem.Model;
using PosSystem.Repository;
using System;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmUserManagement : Form
    {
        string stitle = "User Management";
        readonly PosRepository _posRepository = new PosRepository();

        public frmUserManagement()
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

            var roles = _posRepository.GetRoleTypes();

            cmbUserType.DataSource = roles;
            cmbUserType.DisplayMember = "Type";
            cmbUserType.ValueMember = "Id";
            cmbUserType.SelectedIndex = roles.FindIndex(x => x.Type.ToLower().Trim() == "cashier");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Please provide a User Name.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbUserType.Text))
            {
                MessageBox.Show("Please provide User Type.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbUserType.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please provide a Firstname.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please provide a Lastname", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please provide a Password", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please re-enter Password", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConfirmPassword.Focus();
                return;
            }

            if (txtPassword.Text.ToLower().Trim() != txtConfirmPassword.Text.ToLower().Trim())
            {
                MessageBox.Show("Password confirmation does not match", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConfirmPassword.Focus();
                return;
            }
          
            User user = new User
            {
                UserName = txtUserName.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
                IsActive = true,
                Role = cmbUserType.SelectedIndex + 1
            };

            int result = 0;

            try
            {
                if (radioEntry.Checked)
                {
                    var userCheck = _posRepository.GetUserByUserName(user.UserName);
                    if (userCheck != null && !string.IsNullOrWhiteSpace(userCheck.UserName))
                    {
                        MessageBox.Show("User cannot be added.\n " +
                            "User with UserName " + userCheck.UserName + " already exists.\n " +
                            "You can Update the User instead.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        result = _posRepository.InsertUser(user);
                    }
                }
                else if (radioUpdate.Checked)
                {
                    result = _posRepository.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (result == 1)
            {
                MessageBox.Show("User " + (radioEntry.Checked ? "Entry" : "Update") + " Successful!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                btnLoadUser.Visible = false;
                txtUserName.Enabled = true;
                txtUserName.Text = string.Empty;
                cmbUserType.Enabled = true;
                cmbUserType.Text = string.Empty;
                txtFirstName.Enabled = true;
                txtFirstName.Text = string.Empty;
                txtLastName.Enabled = true;
                txtLastName.Text = string.Empty;
                txtPassword.Enabled = true;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Enabled = true;
                txtConfirmPassword.Text = string.Empty;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
                btnDelete.Visible = false;

                ReadOnlyControls(false);
            }
        }

        private void radioUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUpdate.Checked)
            {                
                lblUpdate.Visible = true;
                lblEntry.Visible = false;
                lblDelete.Visible=false;
                btnLoadUser.Visible = true;
                txtUserName.Enabled = true;
                txtUserName.Text = string.Empty;
                cmbUserType.Enabled = false;
                cmbUserType.Text = string.Empty;
                txtFirstName.Enabled = false;
                txtFirstName.Text = string.Empty;
                txtLastName.Enabled = false;
                txtLastName.Text = string.Empty;
                txtPassword.Enabled = false;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Enabled = false;
                txtConfirmPassword.Text = string.Empty;
                btnSubmit.Visible = true;
                btnDelete.Visible = false;
                btnSubmit.Enabled = false;

                ReadOnlyControls(false);
            }
        }

        private void radioDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDelete.Checked)
            {
                lblDelete.Visible = true;
                lblUpdate.Visible = false;
                lblEntry.Visible = false;
                btnLoadUser.Visible = true;
                txtUserName.Enabled = true;
                txtUserName.Text = string.Empty;
                cmbUserType.Enabled = false;
                cmbUserType.Text = string.Empty;
                txtFirstName.Enabled = false;
                txtFirstName.Text = string.Empty;
                txtLastName.Enabled = false;
                txtLastName.Text = string.Empty;
                txtPassword.Enabled = false;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Enabled = false;
                txtConfirmPassword.Text = string.Empty;
                btnSubmit.Visible = false;
                btnDelete.Visible = true;
                btnDelete.Enabled = false;

                ReadOnlyControls(true);
            }
        }

        private void btnLoadItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Please provide a User Name.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return;
            }

            var user = _posRepository.GetUserByUserName(txtUserName.Text.Trim());

            if (user == null || string.IsNullOrWhiteSpace(user.UserName)) 
            {
                MessageBox.Show("User with User Name " + txtUserName.Text + " not found.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbUserType.Enabled = false;
                cmbUserType.Text = string.Empty;
                return;           
            }

            if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
            {
                EnableControls();

                txtUserName.Text = user.UserName;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtPassword.Text = user.Password;
                txtConfirmPassword.Text = user.Password;
                cmbUserType.SelectedIndex = user.Role - 1;
                btnDelete.Enabled = true;
            }
        }

        private void ClearFields()
        {
            txtUserName.Text = string.Empty;
            cmbUserType.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        private void EnableControls(bool enable = true)
        {
            cmbUserType.Enabled = enable;
            txtFirstName.Enabled = enable;
            txtLastName.Enabled = enable;
            txtPassword.Enabled = enable;
            txtConfirmPassword.Enabled = enable;
            btnSubmit.Enabled = enable;
        }

        private void ReadOnlyControls(bool readOnly = true)
        {        
            txtFirstName.ReadOnly = readOnly;
            txtLastName.ReadOnly = readOnly;
            txtPassword.ReadOnly = readOnly;
            txtConfirmPassword.ReadOnly = readOnly;
            if (readOnly)
            {
                cmbUserType.DropDownStyle = ComboBoxStyle.Simple;
                cmbUserType.Enabled = false;
            }
            else
            {
                cmbUserType.DropDownStyle = ComboBoxStyle.DropDown;
                cmbUserType.Enabled = true;
            }
            cmbUserType.Text = string.Empty;
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
            if (MessageBox.Show($"Are you sure you want to delete User with UserName {txtUserName.Text}?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    result = _posRepository.DeleteUser(txtUserName.Text.Trim());
                }

                if (result == 1)
                {
                    MessageBox.Show($"User with UserName {txtUserName.Text} has been Deleted!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Delete Failed! Please try again.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                return;
            }
        }
    }
}
