using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLySieuThi
{
    public partial class frmThongke : Form
    {
        public frmThongke()
        {
            InitializeComponent();
        }

        private void frmThongke_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLSieuThi;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select sp.tensanpham as 'Tên sản phẩm',SUM(ct.soluong) as 'Số lượng bán ra',SUM(ct.thanhtien) as 'Tổng tiền thu' from chitietHDban ct inner join sanpham sp on sp.ma=ct.SPma group by sp.tensanpham", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            adp.Fill(tb);
            adp.Dispose();
            dgvThongke.DataSource = tb;
            con.Close();
        }
    }
}
