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
    public partial class frmSanpham : Form
    {
        public frmSanpham()
        {
            InitializeComponent();
        }
        private void loadDL()
        {
            Ketnoi.Con.Open();
            DataTable tb = new DataTable();
            string sql = "select ma as 'Mã SP',tensanpham as 'Tên SP',soluongtrongkho as 'Số lượng trong kho',dongianhap as 'Đơn giá nhập', dongiaban as 'Đơn giá bán' from sanpham";
            new SqlDataAdapter(new SqlCommand(sql, Ketnoi.Con)).Fill(tb);
            dgvSanpham.DataSource = tb;
            Ketnoi.Con.Close();
            cellclickk();
        }
        private void cellclickk()
        {
            txtMa.Text = dgvSanpham.CurrentRow.Cells[0].Value.ToString();
            txtTen.Text = dgvSanpham.CurrentRow.Cells[1].Value.ToString();
            txtSoluong.Text = dgvSanpham.CurrentRow.Cells[2].Value.ToString();
            txtDongianhap.Text = dgvSanpham.CurrentRow.Cells[3].Value.ToString();
            txtDongiaban.Text = dgvSanpham.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtSoluong.ReadOnly = txtMa.ReadOnly = txtTen.ReadOnly = txtDongiaban.ReadOnly = txtDongianhap.ReadOnly = false;
            txtMa.Text = txtDongiaban.Text = txtDongianhap.Text = txtTen.Text = txtSoluong.Text = txtTimkiem.Text = "";
        }

        private void btnDanhsach_Click(object sender, EventArgs e)
        {
            txtSoluong.ReadOnly = txtMa.ReadOnly = txtTen.ReadOnly = txtDongiaban.ReadOnly = txtDongianhap.ReadOnly = true;
            txtMa.Text = txtDongiaban.Text = txtDongianhap.Text = txtTen.Text = txtSoluong.Text = txtTimkiem.Text = "";
            loadDL();
        }

        private void btnXacnhanthem_Click(object sender, EventArgs e)
        {
            if (txtMa.TextLength > 0 && txtDongiaban.TextLength > 0 && txtDongianhap.TextLength > 0 && txtSoluong.TextLength > 0 && txtTen.TextLength > 0)
            {
                if ((MessageBox.Show("Bạn muốn thêm sản phẩm này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Ketnoi.Con.Open();
                    try
                    {
                        (new SqlCommand(string.Format("insert into sanpham select N'{0}',N'{1}',N'{2}',N'{3}',N'{4}'", txtMa.Text, txtTen.Text, txtSoluong.Text, txtDongianhap.Text, txtDongiaban.Text), Ketnoi.Con)).ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công");
                        Ketnoi.Con.Close();
                        loadDL();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Ketnoi.Con.Close();
                    }
                }
            }
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            txtSoluong.ReadOnly = txtMa.ReadOnly = txtTen.ReadOnly = txtDongiaban.ReadOnly = txtDongianhap.ReadOnly = true;
            txtMa.Text = txtDongiaban.Text = txtDongianhap.Text = txtTen.Text = txtSoluong.Text = txtTimkiem.Text = "";
            loadDL();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtSoluong.ReadOnly = txtTen.ReadOnly = txtDongiaban.ReadOnly = txtDongianhap.ReadOnly = false;
            cellclickk();
        }

        private void dgvSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cellclickk();
        }

        private void btnXacnhansua_Click(object sender, EventArgs e)
        {
            if (txtMa.TextLength > 0 && txtDongiaban.TextLength > 0 && txtDongianhap.TextLength > 0 && txtSoluong.TextLength > 0 && txtTen.TextLength > 0)
            {
                if (MessageBox.Show("Bạn muốn sửa thông tin sản phẩm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Ketnoi.Con.Open();
                    string sua = string.Format("update sanpham set tensanpham=N'{1}', soluongtrongkho=N'{2}', dongianhap=N'{3}', dongiaban=N'{4}' where ma='{0}'", txtMa.Text, txtTen.Text, txtSoluong.Text, txtDongianhap.Text, txtDongiaban.Text);
                    SqlCommand cmd = new SqlCommand(sua, Ketnoi.Con);
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sửa thành công");
                        Ketnoi.Con.Close();
                        loadDL();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Ketnoi.Con.Close();
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa thông tin?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Ketnoi.Con.Open();
                string xoa = string.Format("delete from sanpham where ma='{0}'", dgvSanpham.CurrentRow.Cells[0].Value.ToString());
                SqlCommand cmd = new SqlCommand(xoa, Ketnoi.Con);
                cmd.CommandType = CommandType.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công");
                    Ketnoi.Con.Close();
                    loadDL();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Ketnoi.Con.Close();
                }
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                Ketnoi.Con.Open();
                string timkiem = "select ma as 'Mã SP',tensanpham as 'Tên SP',soluongtrongkho as 'Số lượng trong kho', dongianhap as 'Đơn giá nhập', dongiaban as 'Đơn giá bán' from sanpham where ma like N'%" + txtTimkiem.Text + "%' or soluongtrongkho like N'%" + txtTimkiem.Text + "%' or tensanpham like N'%" + txtTimkiem.Text + "%' or dongianhap like N'%" + txtTimkiem.Text + "%' or dongiaban like N'%" + txtTimkiem.Text + "%'";
                SqlCommand cmd = new SqlCommand(timkiem, Ketnoi.Con);
                //cmd.Parameters.Add(new SqlParameter("@tukhoa", txtTimkiem.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);
                dgvSanpham.DataSource = tb;
                Ketnoi.Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Ketnoi.Con.Close();
            }
            txtTimkiem.Focus();
        }
    }
}
