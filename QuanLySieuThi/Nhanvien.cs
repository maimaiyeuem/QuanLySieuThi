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
    public partial class frmNhanvien : Form
    {
        public frmNhanvien()
        {
            InitializeComponent();
        }
        private void loadDL()
        {
            Ketnoi.Con.Open();
            string sql = "select ma as 'Mã NV',ten as 'Tên',gioitinh as 'Giới tính', chucvu as 'Chức vụ',convert(char(10),ngaysinh,103) as 'Ngày sinh', diachi as 'Địa chỉ', dienthoai as 'Số điện thoại' from nhanvien";
            DataTable tb = new DataTable();
            new SqlDataAdapter(new SqlCommand(sql, Ketnoi.Con)).Fill(tb);
            dgvNhanvien.DataSource = tb;
            Ketnoi.Con.Close();
        }

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            txtChucvu.ReadOnly = txtMa.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = true;
            dtpNgaysinh.Enabled = cboGioitinh.Enabled = false;
            txtChucvu.Text = cboGioitinh.Text = txtMa.Text = txtTen.Text = txtSDT.Text = txtDiachi.Text = "";
            loadDL();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtChucvu.ReadOnly = txtMa.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = false;
            dtpNgaysinh.Enabled = cboGioitinh.Enabled = true;
            txtChucvu.Text = cboGioitinh.Text = txtMa.Text = txtTen.Text = txtSDT.Text = txtDiachi.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtChucvu.ReadOnly = txtMa.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = true;
            dtpNgaysinh.Enabled = cboGioitinh.Enabled = false;
            txtTimkiem.Text = txtChucvu.Text = cboGioitinh.Text = txtMa.Text = txtTen.Text = txtSDT.Text = txtDiachi.Text = "";
            loadDL();
        }

        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dgvNhanvien.CurrentRow.Cells[0].Value.ToString();
            txtTen.Text = dgvNhanvien.CurrentRow.Cells[1].Value.ToString();
            cboGioitinh.Text = dgvNhanvien.CurrentRow.Cells[2].Value.ToString();
            txtChucvu.Text = dgvNhanvien.CurrentRow.Cells[3].Value.ToString();
            txtDiachi.Text = dgvNhanvien.CurrentRow.Cells[5].Value.ToString();
            txtSDT.Text = dgvNhanvien.CurrentRow.Cells[6].Value.ToString();
            dtpNgaysinh.Text = dgvNhanvien.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnXacnhanthem_Click(object sender, EventArgs e)
        {
            if (txtChucvu.TextLength > 0 && txtMa.TextLength > 0 && txtDiachi.TextLength > 0 && cboGioitinh.Text.Length > 0 && txtSDT.TextLength > 0 && txtTen.TextLength > 0)
            {
                if ((MessageBox.Show("Bạn muốn thêm khách hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Ketnoi.Con.Open();
                    try
                    {
                        (new SqlCommand(string.Format("insert into nhanvien select '{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}','{6}'", txtMa.Text, txtTen.Text, cboGioitinh.Text, txtChucvu.Text, dtpNgaysinh.Value.ToString("MM-dd-yyyy"), txtDiachi.Text, txtSDT.Text), Ketnoi.Con)).ExecuteNonQuery();
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMa.ReadOnly = true;
            txtChucvu.ReadOnly = txtTen.ReadOnly = txtDiachi.ReadOnly = txtSDT.ReadOnly = false;
            dtpNgaysinh.Enabled = cboGioitinh.Enabled = true;
        }

        private void btnXacnhansua_Click(object sender, EventArgs e)
        {
            if (txtChucvu.TextLength > 0 && txtDiachi.TextLength > 0 && cboGioitinh.Text.Length > 0 && txtSDT.TextLength > 0 && txtTen.TextLength > 0)
            {
                if (MessageBox.Show("Bạn muốn sửa thông tin?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Ketnoi.Con.Open();
                    string sua = string.Format("update nhanvien set ten=N'{1}', gioitinh=N'{2}', chucvu=N'{3}', ngaysinh='{4}', diachi=N'{5}', dienthoai=N'{6}' where ma=N'{0}'", txtMa.Text, txtTen.Text, cboGioitinh.Text, txtChucvu.Text, dtpNgaysinh.Value.ToString("MM-dd-yyyy"), txtDiachi.Text, txtSDT.Text);
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
                string xoa = string.Format("delete from nhanvien where ma='{0}'", dgvNhanvien.CurrentRow.Cells[0].Value.ToString());
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
                string timkiem = "select ma as 'Mã NV',ten as 'Tên',gioitinh as 'Giới tính', chucvu as 'Chức vụ',convert(char(10),ngaysinh,103) as 'Ngày sinh', diachi as 'Địa chỉ', dienthoai as 'Số điện thoại' from nhanvien where ma like N'%" + txtTimkiem.Text + "%' or ten like N'%" + txtTimkiem.Text + "%' or gioitinh like N'%" + txtTimkiem.Text + "%' or chucvu like N'%" + txtTimkiem.Text + "%' or diachi like N'%" + txtTimkiem.Text + "%' or dienthoai like N'%" + txtTimkiem.Text + "%' or convert(char(10),ngaysinh,103) like '%" + txtTimkiem.Text + "%'";
                SqlCommand cmd = new SqlCommand(timkiem, Ketnoi.Con);
                //cmd.Parameters.Add(new SqlParameter("@tukhoa", txtTimkiem.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);
                dgvNhanvien.DataSource = tb;
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
