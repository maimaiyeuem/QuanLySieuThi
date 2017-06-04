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
    public partial class frmDangnhap : Form
    {
        public frmDangnhap()
        {
            InitializeComponent();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                Ketnoi.Con.Open();
                string sql = "select count(*) from dangnhap where taikhoan=@acc and matkhau=@pass";
                SqlCommand command = new SqlCommand(sql, Ketnoi.Con);
                command.Parameters.Add(new SqlParameter("@acc", txtTaikhoan.Text));
                command.Parameters.Add(new SqlParameter("@pass", txtMatkhau.Text));
                int x = (int)command.ExecuteScalar();
                Ketnoi.Con.Close();
                if (x == 1)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo");
                    this.Hide();
                    (new frmHethong()).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại", "Thông báo");
                    MessageBox.Show("Sai thong tin");
                    txtTaikhoan.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkHienthimatkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienthimatkhau.Checked == true)
            {
                txtMatkhau.UseSystemPasswordChar = false;
            }
            else txtMatkhau.UseSystemPasswordChar = true;
        }

        private void frmDangnhap_Load(object sender, EventArgs e)
        {
            txtTaikhoan.Text = "admin";
            txtMatkhau.Text = "admin";
        }
    }
}
