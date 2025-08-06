using PosSystem.Repository;
using System;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmAddDiscountType : Form
    {
        //SqlConnection cn = new SqlConnection();
        //SqlCommand cm = new SqlCommand();
        //SqlDataReader dr;
        //DBConnection dbcon = new DBConnection();
        string stitle = "Pos System";
        frmPOS f;
        readonly PosRepository _posRepository = new PosRepository();

        public frmAddDiscountType(frmPOS frm)
        {
            InitializeComponent();
            f = frm;
            //cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiscountType.Text))
            {
                MessageBox.Show("Please provide the new Discount Type.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int result = 0;

            try
            {
                result = _posRepository.InsertDiscountType(txtDiscountType.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (result == 1)
            {
                MessageBox.Show("Discount Type creation Successful!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Dispose();
            }
            else
            {
                MessageBox.Show("Discount Type creation Failed! Please try again.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtDiscountType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnConfirm_Click(sender, e);
            }
        }
    }
}
