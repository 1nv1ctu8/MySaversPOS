using PosSystem.Model;
using PosSystem.Repository;
using System;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmAddUser : Form
    {
        //SqlConnection cn = new SqlConnection();
        //SqlCommand cm = new SqlCommand();
        //SqlDataReader dr;
        //DBConnection dbcon = new DBConnection();
        string stitle = "Pos System";
        frmPOS f;
        readonly PosRepository _posRepository = new PosRepository();

        public frmAddUser(frmPOS frm)
        {
            InitializeComponent();
            f = frm;
            //cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;

            var roleTypes = _posRepository.GetRoleTypes();
            cmbUserType.DataSource = roleTypes;
            cmbUserType.DisplayMember = "Type";
            cmbUserType.ValueMember = "Id";

            cmbUserType.SelectedIndex = 3;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Please provide a User Name", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please provide your First Name", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please provide your Last Name", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please provide a Password", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please confirm your Password", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPassword.Text.ToLower().Trim() != txtConfirmPassword.Text.ToLower().Trim())
            {
                MessageBox.Show("Passwords do not match. Try again.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            User user = new User
            {
                UserName = txtUserName.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Role = cmbUserType.SelectedIndex + 1,
                Password = txtPassword.Text
            };

            int result = 0;

            try
            {
                result = _posRepository.InsertUser(user);
            }
            catch (Exception ex)
            {
                //cn.Close();
                MessageBox.Show(ex.Message);
            }

            if (result == 1)
            {
                MessageBox.Show("User creation Successful!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Dispose();
            }
            else
            {
                MessageBox.Show("User creation Failed! Please try again.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirm_Click(sender, e);
            }
        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
