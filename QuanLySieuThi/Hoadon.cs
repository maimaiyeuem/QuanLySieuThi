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
    public partial class frmHoadon : Form
    {
        public frmHoadon()
        {
            InitializeComponent();
        }
        List<ChitietHDban> list = new List<ChitietHDban>();
        private void loadData(string str)
        {
            switch (str)
            {
                case "hoadon":
                    {
                        loadHoadon();
                        break;
                    }
                case "nhanvien":
                    {
                        loadNhanvien();
                        txtTennhanvien.Text = dgvNhanvien.Rows[0].Cells[1].Value.ToString();
                        break;
                    }
                case "khachhang":
                    {
                        loadKhachhang();
                        txtTenkhachhang.Text = dgvKhachhang.Rows[0].Cells[1].Value.ToString();
                        txtDiachi.Text = dgvKhachhang.Rows[0].Cells[3].Value.ToString();
                        txtDienthoai.Text = dgvKhachhang.Rows[0].Cells[4].Value.ToString();
                        break;
                    }
                case "sanpham":
                    {
                        loadSanpham();
                        txtTenhang.Text = dgvSanpham.Rows[0].Cells[1].Value.ToString();
                        txtDongia.Text = dgvSanpham.Rows[0].Cells[4].Value.ToString();
                        break;
                    }
                default:
                    {

                        loadNhanvien();
                        txtTennhanvien.Text = dgvNhanvien.Rows[0].Cells[1].Value.ToString();

                        loadKhachhang();
                        txtTenkhachhang.Text = dgvKhachhang.Rows[0].Cells[1].Value.ToString();
                        txtDiachi.Text = dgvKhachhang.Rows[0].Cells[3].Value.ToString();
                        txtDienthoai.Text = dgvKhachhang.Rows[0].Cells[4].Value.ToString();

                        loadSanpham();
                        txtTenhang.Text = dgvSanpham.Rows[0].Cells[1].Value.ToString();
                        txtDongia.Text = dgvSanpham.Rows[0].Cells[4].Value.ToString();

                        break;
                    }
            }
        }

        private void loadHoadon()
        {
            Ketnoi.Con.Open();
            DataTable tbHoadon = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from HDban", Ketnoi.Con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(tbHoadon);
            ad.Dispose();
            dgvHoadon.DataSource = tbHoadon;
            Ketnoi.Con.Close();
        }

        private void loadNhanvien()
        {
            Ketnoi.Con.Open();
            DataTable tbNhanvien = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from nhanvien where ma='" + cboManhanvien.Text + "'", Ketnoi.Con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(tbNhanvien);
            ad.Dispose();
            Ketnoi.Con.Close();
            dgvNhanvien.DataSource = tbNhanvien;
        }

        private void loadKhachhang()
        {
            Ketnoi.Con.Open();
            DataTable tbKhachhang = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from khachhang where ma='" + cboMakhachhang.Text + "'", Ketnoi.Con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(tbKhachhang);
            ad.Dispose();
            dgvKhachhang.DataSource = tbKhachhang;
            Ketnoi.Con.Close();
        }

        private void loadSanpham()
        {
            //var date = DateTime.Parse("06:45 PM");lblTongtien.Text = date.ToString();
            Ketnoi.Con.Open();
            DataTable tbSanpham = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from sanpham where ma='" + cboMahang.Text + "'", Ketnoi.Con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(tbSanpham);
            ad.Dispose();
            dgvSanpham.DataSource = tbSanpham;
            Ketnoi.Con.Close();
        }

        private void Taodatagridview()
        {
            dgvThemvaohoadon.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn Mahang = new DataGridViewTextBoxColumn();
            Mahang.DataPropertyName = "SPma";
            Mahang.HeaderText = "Mã SP";

            DataGridViewTextBoxColumn Tenhang = new DataGridViewTextBoxColumn();
            Tenhang.DataPropertyName = "Tensanpham";
            Tenhang.HeaderText = "Tên sản phẩm";

            DataGridViewTextBoxColumn Soluong = new DataGridViewTextBoxColumn();
            Soluong.DataPropertyName = "Soluong";
            Soluong.HeaderText = "Số lượng";

            DataGridViewTextBoxColumn Dongia = new DataGridViewTextBoxColumn();
            Dongia.DataPropertyName = "Dongiaban";
            Dongia.HeaderText = "Đơn giá";

            DataGridViewTextBoxColumn Thanhtien = new DataGridViewTextBoxColumn();
            Thanhtien.DataPropertyName = "Thanhtien";
            Thanhtien.HeaderText = "Thành tiền";

            dgvThemvaohoadon.Columns.Add(Mahang);
            dgvThemvaohoadon.Columns.Add(Tenhang);
            dgvThemvaohoadon.Columns.Add(Soluong);
            dgvThemvaohoadon.Columns.Add(Dongia);
            dgvThemvaohoadon.Columns.Add(Thanhtien);
        }

        private void loadThemvaohoadon()
        {
            if (list.Exists(x => x.SPma == cboMahang.Text))
            {
                list.RemoveAt(list.FindIndex(x => x.SPma == cboMahang.Text));
                list.Add(new ChitietHDban(cboMahang.Text, txtTenhang.Text, txtSoluong.Text, txtDongia.Text, txtThanhtien.Text));
            }
            else
            {
                list.Add(new ChitietHDban(cboMahang.Text, txtTenhang.Text, txtSoluong.Text, txtDongia.Text, txtThanhtien.Text));
            }
            dgvThemvaohoadon.DataSource = null;
            dgvThemvaohoadon.DataSource = list;

            long tien = 0;
            for (int i = 0; i <= dgvThemvaohoadon.Rows.Count - 1; i++)
            {
                tien = tien + Int64.Parse(dgvThemvaohoadon.Rows[i].Cells[4].Value.ToString());
            }
            lblTongtien.Text = tien.ToString();
        }

        private void frmHoadon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLSieuThiDataSet.sanpham' table. You can move, or remove it, as needed.
            this.sanphamTableAdapter.Fill(this.qLSieuThiDataSet.sanpham);
            // TODO: This line of code loads data into the 'qLSieuThiDataSet.nhanvien' table. You can move, or remove it, as needed.
            this.nhanvienTableAdapter.Fill(this.qLSieuThiDataSet.nhanvien);
            // TODO: This line of code loads data into the 'qLSieuThiDataSet.khachhang' table. You can move, or remove it, as needed.
            this.khachhangTableAdapter.Fill(this.qLSieuThiDataSet.khachhang);
            dtpNgayban.Format = DateTimePickerFormat.Custom;
            dtpNgayban.CustomFormat = "hh:mm:ss tt --- dd/MM/yyyy";
            lblThongbao.Visible = lblCanhbao.Visible = false;
            txtMahoadon.Text = "HDB_" + dtpNgayban.Value.ToString("ddMMyyyyhhmmsstt");
            txtMahoadon.ReadOnly = txtTenkhachhang.ReadOnly = txtTennhanvien.ReadOnly = txtDiachi.ReadOnly = txtDienthoai.ReadOnly = txtTenhang.ReadOnly = txtDongia.ReadOnly = txtThanhtien.ReadOnly = true;
            loadData("all");
            list.Clear();
            Taodatagridview();
            dgvHoadon.Visible = false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            dtpNgayban.Value = DateTime.Now;
        }

        private void cboMakhachhang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData("khachhang");
        }

        private void cboManhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData("nhanvien");
        }

        private void cboMahang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData("sanpham");
            if (txtSoluong.Text != "")
                txtThanhtien.Text = (Int64.Parse(txtSoluong.Text) * Int64.Parse(txtDongia.Text)).ToString();
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (Int32.TryParse(txtSoluong.Text, out a))
            {
                lblCanhbao.Visible = false;
                txtThanhtien.Text = (Int64.Parse(txtSoluong.Text) * Int64.Parse(txtDongia.Text)).ToString();
            }
            else
            {
                lblCanhbao.Visible = true;
                lblCanhbao.Text = "Bạn cần phải nhập số!";
            }
        }

        private void btnThemvaohoadon_Click(object sender, EventArgs e)
        {
            dgvHoadon.Visible = false;
            dgvThemvaohoadon.Visible = true;
            if (txtSoluong.Text == "")
            {
                lblCanhbao.Visible = true;
                lblCanhbao.Text = "Bạn phải nhập gì đó vào đây chứ!";
            }
            else
            {
                loadThemvaohoadon();
                lblThongbao.Text = "Đã thêm vào đơn hàng";
                lblThongbao.Visible = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmHoadon.ActiveForm.Close();
        }

        private void btnLuuhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connec = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLSieuThi;Integrated Security=True");
                connec.Open();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand(string.Format("insert into HDban select '{0}','{1}','{2}','{3}','{4}'", txtMahoadon.Text, cboManhanvien.Text, dtpNgayban.Value.ToString("MM-dd-yyyy HH:mm:ss"), cboMakhachhang.Text, lblTongtien.Text), connec);
                cmd2.ExecuteNonQuery();
                cmd.Connection = connec;
                for (int i = 0; i <= dgvThemvaohoadon.Rows.Count - 1; i++)
                {
                    string sql = "insert into chitietHDban values('" + txtMahoadon.Text + "','" + dgvThemvaohoadon.Rows[i].Cells[0].Value.ToString() + "','" + dgvThemvaohoadon.Rows[i].Cells[3].Value + "','" + dgvThemvaohoadon.Rows[i].Cells[2].Value + "','" + dgvThemvaohoadon.Rows[i].Cells[4].Value + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                lblThongbao2.Visible = true;
                lblThongbao2.Text = "Đã thêm hóa đơn!";
                MessageBox.Show("Số tiền cần thanh toán là: " + lblTongtien.Text + "", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoakhoihoadon_Click(object sender, EventArgs e)
        {
            if (dgvThemvaohoadon.Visible == true && list.Count != 0)
            {
                list.RemoveAt(list.FindIndex(x => x.SPma == cboMahang.Text));
                dgvThemvaohoadon.DataSource = null;
                dgvThemvaohoadon.DataSource = list;
            }
        }

        private void dgvThemvaohoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvThemvaohoadon.DataSource == null) { }
            else
            {
                cboMahang.Text = dgvThemvaohoadon.CurrentRow.Cells[0].Value.ToString();
                txtTenhang.Text = dgvThemvaohoadon.CurrentRow.Cells[1].Value.ToString();
                txtSoluong.Text = dgvThemvaohoadon.CurrentRow.Cells[2].Value.ToString();
                txtDongia.Text = dgvThemvaohoadon.CurrentRow.Cells[3].Value.ToString();
                txtThanhtien.Text = dgvThemvaohoadon.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void btnThemhoadonmoi_Click(object sender, EventArgs e)
        {
            dgvHoadon.Visible = false;
            dgvThemvaohoadon.Visible = true;
            list.Clear();
            dgvThemvaohoadon.DataSource = null;
            dgvThemvaohoadon.DataSource = list;
            dtpNgayban.Value = DateTime.Now;
            txtMahoadon.Text = "HDB_" + dtpNgayban.Value.ToString("ddMMyyyyhhmmsstt");
        }

        private void btnHuyhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLSieuThi;Integrated Security=True");
                conn.Open();
                string sqll = "delete from ChitietHDban where HDbanma='" + txtMHD.Text + "'";
                new SqlCommand(sqll, conn).ExecuteNonQuery();
                sqll = "delete from HDban where ma='" + txtMHD.Text + "'";
                new SqlCommand(sqll, conn).ExecuteNonQuery();
                conn.Close();
                loadHoadon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            dgvThemvaohoadon.Visible = false;
            dgvHoadon.Visible = true;
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLSieuThi;Integrated Security=True");
            con.Open();
            DataTable tb = new DataTable();
            new SqlDataAdapter(new SqlCommand("select ma as 'Mã hóa đơn',nhanvienma as 'Mã nhân viên',ngayban as 'Ngày bán',khachhangma as 'Mã khách hàng',tongtien as 'Tổng tiền thanh toán' from HDban", con)).Fill(tb);
            dgvHoadon.DataSource = null;
            dgvHoadon.DataSource = tb;
            con.Close();
        }

        private void dgvHoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMHD.Text = dgvHoadon.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
