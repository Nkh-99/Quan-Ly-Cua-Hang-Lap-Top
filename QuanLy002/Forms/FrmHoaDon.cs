using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy002.BS_Layer;

namespace QuanLy002.Forms
{
    public partial class FrmHoaDon : Form
    {
        bool Them;
        string err;
        DataTable dt;
        double worth;
        public FrmHoaDon()
        {
            InitializeComponent();
            txtMaHDBan.Enabled = !true;
            cboManhanvien.Enabled = !true;
            txtTennhanvien.Enabled = !true;
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            cboManhanvien.Text = FrmLogin.MaNV;
            txtTennhanvien.Text = QuanlyContext.MyContext.NhanViens.Find(FrmLogin.MaNV).TenNV;

            btnThemmoi.Enabled = true;
            btnXoa.Enabled = true;
            btnTimkiem.Enabled = true;
            btnHuy.Enabled = !true;
            btnLuu.Enabled = !true;
            btnThemhang.Enabled = !true;

            InitCBox();
            InitTempTable();

            if (txtMaHDBan.Text.Trim() != "")
            {
                cboMaHDBan.Text = txtMaHDBan.Text;
                btnTimkiem.PerformClick();
            }
        }

        void InvoiceLoad()
        {
            try
            {
                string InvoiceID = cboMaHDBan.SelectedValue.ToString();
                HoaDon Invoice = BLHoaDon.GetInvoiceById(InvoiceID);
                txtMaHDBan.Text = Invoice.MaHD;
                dtpNgayBan.Value = Invoice.Ngayban;
                txtTongtien.Text = Invoice.Tongtien.ToString();
                cboMakhach.SelectedValue = Invoice.MaKH;
                cboManhanvien.Text = Invoice.MaNV;

                foreach (var item in BLChitietHD.GetChitietHDByMaHD(InvoiceID))
                {
                    dt.Rows.Add(item.MaHD, item.MaHang, item.Soluong, item.Dongia, item.Giamgia, item.Thanhtien);
                }
                DataGridView.DataSource = dt;
                DataGridView.AutoResizeColumns();
            }
            catch
            {
                MessageBox.Show("Không lấy được dữ liệu. Lỗi rồi!!!");
            }
        }

        void InitTempTable()
        {
            dt = new DataTable();
            dt.Columns.Add(nameof(HoaDon.MaHD));
            dt.Columns.Add(nameof(ChitietHD.MaHang));
            dt.Columns.Add(nameof(Hang.Soluong));
            dt.Columns.Add(nameof(ChitietHD.Dongia));
            dt.Columns.Add(nameof(ChitietHD.Giamgia));
            dt.Columns.Add(nameof(ChitietHD.Thanhtien));
        }

        void InitCBox()
        {
            try
            {
                BLHang.CBoxItems(cboMahang);
                BLKhachHang.CBoxCustomer(cboMakhach);              
                BLHoaDon.CBoxHoaDon(cboMaHDBan, FrmLogin.MaNV, FrmLogin.Level); // nhan vien: NV ban hang chi xem duoc hoa don co MaNV cua minh
                                                        // admin =1, NV ke toan =3
            }
            catch (Exception ex)
            {
                MessageBox.Show(Environment.NewLine + ex.Message, "ERROR:");
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboMaHDBan.SelectedValue == null)
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm / Không có mã hoá đơn này!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHDBan.Focus();
                return;
            }
            InvoiceLoad();
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            Them = true;
            btnThemmoi.Enabled = !true;
            btnXoa.Enabled = !true;
            btnTimkiem.Enabled = !true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThemhang.Enabled = true;

            worth = 0;
            dt.Rows.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool test = BLHoaDon.DeleteInvoice(cboMaHDBan.SelectedValue.ToString(), ref err);
                if (test == false)
                {
                    MessageBox.Show("Không xoá được. Lỗi rồi!" + Environment.NewLine
                       + err, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnReload.PerformClick();
                btnHuy.PerformClick();
                MessageBox.Show("Xoá - Hoàn tất!");
            }
            else
            {
                MessageBox.Show("Ngừng việc xóa Hoá Đơn!");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Them = false;
            btnThemmoi.Enabled = true;
            btnXoa.Enabled = true;
            btnTimkiem.Enabled = true;
            btnHuy.Enabled = !true;
            btnLuu.Enabled = !true;
            btnThemhang.Enabled = !true;

            cboMakhach.ResetText();
            txtTenkhach.ResetText();
            txtDiachi.ResetText();
            txtDienthoai.ResetText();

            cboMahang.ResetText();
            txtTenhang.ResetText();
            txtDongiaban.ResetText();
            txtTon.ResetText();
            txtSoluong.ResetText();
            txtNote.ResetText();
            txtGiamgia.ResetText();
            txtThanhtien.ResetText();
            txtTongtien.ResetText();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if (cboMakhach.SelectedValue == null)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMakhach.Focus();
                    return;
                }
                if (DataGridView.Rows.Count < 1)
                {
                    MessageBox.Show("Giỏ hàng vẫn còn trống, hãy thêm sản phẩm vào giỏ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMahang.Focus();
                    return;
                }
                
                if (BLHoaDon.InsertInvoice(cboManhanvien.Text, cboMakhach.SelectedValue.ToString(), dt, ref err))
                {
                    MessageBox.Show("Thêm hoá đơn mới thành công, hãy thanh toán hoá đơn!", "Successful:",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(err, "ERROR:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                worth = 0;
                InitCBox();
                btnHuy.PerformClick();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            InitCBox();
            btnHuy.PerformClick();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (Them == true)
            {
                if (MessageBox.Show("Hoá đơn chưa được lưu <Bạn có chắc muốn thoát ngay không?>", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        private void btnThemhang_Click(object sender, EventArgs e)
        {
            if (cboMahang.SelectedValue == null)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMahang.Focus();
                return;
            }
            try
            {
                worth += Convert.ToDouble(txtThanhtien.Text);
                if (Convert.ToDouble(txtTon.Text) < Convert.ToDouble(txtSoluong.Text))
                {
                    txtSoluong.Focus();
                    return;
                }
            }
            catch
            {
                txtSoluong.Focus();
                return;
            }
            txtTongtien.Text = worth.ToString();
            int i;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][nameof(ChitietHD.MaHang)].ToString() == cboMahang.SelectedValue.ToString())
                {
                    double upQuantity = Convert.ToDouble(dt.Rows[i][nameof(Hang.Soluong)]) + Convert.ToDouble(txtSoluong.Text);
                    dt.Rows[i][nameof(Hang.Soluong)] = upQuantity.ToString();
                    double upThanhtien = Convert.ToDouble(dt.Rows[i][nameof(ChitietHD.Thanhtien)]) + Convert.ToDouble(txtThanhtien.Text);
                    dt.Rows[i][nameof(ChitietHD.Thanhtien)] = upThanhtien.ToString();
                    break;
                }
            }
            if (i >= dt.Rows.Count)
                dt.Rows.Add("temp", cboMahang.SelectedValue.ToString(), txtSoluong.Text, txtDongiaban.Text, txtGiamgia.Text, txtThanhtien.Text);
            DataGridView.DataSource = dt;
            DataGridView.AutoResizeColumns();
            MessageBox.Show("Đã thêm xong!");
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoluong.Text == "")
                sl = 0;
            else
            {
                if (Double.TryParse(txtSoluong.Text, out sl) == false)
                {
                    MessageBox.Show("Lỗi! Hãy nhập lại số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoluong.Text = "";
                    txtSoluong.Focus();
                    return;
                }
            }
            if (txtGiamgia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamgia.Text);
            if (txtDongiaban.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDongiaban.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhtien.Text = tt.ToString();
        }

        private void txtGiamgia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoluong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoluong.Text);
            if (txtGiamgia.Text == "")
                gg = 0;
            else
            {
                if (Double.TryParse(txtGiamgia.Text, out gg) == false)
                {
                    MessageBox.Show("Lỗi! Hãy nhập lại số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiamgia.Text = "";
                    txtGiamgia.Focus();
                    return;
                }
            }
            if (txtDongiaban.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDongiaban.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhtien.Text = tt.ToString();
        }

        private void cboMakhach_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboMakhach.SelectedValue == null)
                return;
            Khach cus = BLKhachHang.GetKhachById(cboMakhach.SelectedValue.ToString());
            txtTenkhach.Text = cus.TenKH;
            txtDiachi.Text = cus.Diachi;
            txtDienthoai.Text = cus.Dienthoai;
        }
     
        private void cboMahang_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboMahang.SelectedValue == null)
                return;

            Hang items = BLHang.GetItemsById(cboMahang.SelectedValue.ToString());
            txtTenhang.Text = items.TenHang;
            txtDongiaban.Text = items.GiaBan.ToString();
            txtTon.Text = items.Soluong.ToString();
            txtNote.Text = items.Ghichu;
        }

        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (Them)
            {
                int sizEdata = DataGridView.Rows.Count;
                if (sizEdata < 1)
                {
                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int r = DataGridView.CurrentCell.RowIndex;
                if (r < 0 || r >= DataGridView.RowCount - 1) return;
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    double newTotal = Convert.ToDouble(txtTongtien.Text);
                    double price = Convert.ToDouble(dt.Rows[r][nameof(ChitietHD.Thanhtien)]);
                    newTotal -= price;
                    dt.Rows[r].Delete();
                    txtTongtien.Text = newTotal.ToString();
                    
                    DataGridView.DataSource = dt;
                    DataGridView.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("Phù may quá, bạn đã không xóa hàng này!");
                }
            }
        }

       
    }
}
