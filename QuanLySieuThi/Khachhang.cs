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
    public partial class frmKhachhang : Form
    {
        public frmKhachhang()
        {
            InitializeComponent();
        }
        private void loadDL()
        {
            Ketnoi.Con.Open();
            string sql = "select * from khachhang";
            DataTable tb = new DataTable();
            new SqlDataAdapter(new SqlCommand(sql, Ketnoi.Con)).Fill(tb);
            dgvKhachhang.DataSource = tb;
            Ketnoi.Con.Close();
        }

        private void frmKhachhang_Load(object sender, EventArgs e)
        {
            txtMa.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = true;
            cboGioitinh.Enabled = false;
            cboGioitinh.Text = txtMa.Text = txtTen.Text = txtSDT.Text = txtDiachi.Text = "";
            loadDL();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMa.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = false;
            cboGioitinh.Enabled = true;
            cboGioitinh.Text = txtMa.Text = txtTen.Text = txtSDT.Text = txtDiachi.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMa.ReadOnly = true;
            txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = false;
            cboGioitinh.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa thông tin?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Ketnoi.Con.Open();
                string xoa = string.Format("delete from khachhang where ma='{0}'", dgvKhachhang.CurrentRow.Cells[0].Value.ToString());
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

        private void btnDanhsach_Click(object sender, EventArgs e)
        {
            txtMa.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = true;
            cboGioitinh.Enabled = false;
            cboGioitinh.Text = txtMa.Text = txtTen.Text = txtSDT.Text = txtDiachi.Text = "";
            loadDL();
        }

        private void dgvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dgvKhachhang.CurrentRow.Cells[0].Value.ToString();
            txtTen.Text = dgvKhachhang.CurrentRow.Cells[1].Value.ToString();
            cboGioitinh.Text = dgvKhachhang.CurrentRow.Cells[2].Value.ToString();
            txtDiachi.Text = dgvKhachhang.CurrentRow.Cells[3].Value.ToString();
            txtSDT.Text = dgvKhachhang.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnXacnhanthem_Click(object sender, EventArgs e)
        {
            if (txtMa.TextLength > 0 && txtDiachi.TextLength > 0 && cboGioitinh.Text.Length > 0 && txtSDT.TextLength > 0 && txtTen.TextLength > 0)
            {
                if ((MessageBox.Show("Bạn muốn thêm khách hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Ketnoi.Con.Open();
                    try
                    {
                        (new SqlCommand(string.Format("insert into khachhang select '{0}',N'{1}',N'{2}',N'{3}',N'{4}'", txtMa.Text, txtTen.Text, cboGioitinh.Text, txtDiachi.Text, txtSDT.Text), Ketnoi.Con)).ExecuteNonQuery();
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

        private void btnXacnhansua_Click(object sender, EventArgs e)
        {
            if (txtDiachi.TextLength > 0 && cboGioitinh.Text.Length > 0 && txtSDT.TextLength > 0 && txtTen.TextLength > 0)
            {
                if (MessageBox.Show("Bạn muốn sửa thông tin?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Ketnoi.Con.Open();
                    string sua = string.Format("update khachhang set ten=N'{1}', gioitinh=N'{2}', diachi=N'{3}', dienthoai=N'{4}' where ma='{0}'", txtMa.Text, txtTen.Text, cboGioitinh.Text, txtDiachi.Text, txtSDT.Text);
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

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                Ketnoi.Con.Open();
                string timkiem = "select ma as 'Mã KH',ten as 'Tên',gioitinh as 'Giới tính', diachi as 'Địa chỉ', dienthoai as 'Số điện thoại' from khachhang where ma like N'%" + txtTimkiem.Text + "%' or ten like N'%" + txtTimkiem.Text + "%' or gioitinh like N'%" + txtTimkiem.Text + "%' or diachi like N'%" + txtTimkiem.Text + "%' or dienthoai like N'%" + txtTimkiem.Text + "%'";
                SqlCommand cmd = new SqlCommand(timkiem, Ketnoi.Con);
                //cmd.Parameters.Add(new SqlParameter("@tukhoa", txtTimkiem.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);
                dgvKhachhang.DataSource = tb;
                Ketnoi.Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Ketnoi.Con.Close();
            }
        }
    }
}
