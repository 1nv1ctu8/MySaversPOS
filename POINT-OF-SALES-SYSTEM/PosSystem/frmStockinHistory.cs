using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PosSystem
{
    public partial class frmStockinHistory : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "PosSystem";

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearchProductStokin frm = new frmSearchProductStokin(this);
            frm.LoadProduct();
            frm.ShowDialog();
        }

        private void LoadStockHistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from vwStockin where cast(sdate as date) between '"+date1.Value.ToShortDateString()+"' and '"+date2.Value.ToShortDateString()+"' and status like 'Done'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(),DateTime .Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr["vender"].ToString());
            }
            dr.Close();
            cn.Close();

        }
    }
}
