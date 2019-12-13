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
    public partial class FrmHang : Form
    {
        string errr;
        bool Them;
        public FrmHang()
        {
            InitializeComponent();
            txtMahang.Enabled = !true;
        }

        private void FrmHang_Load(object sender, EventArgs e)
        {
            BLNhaCC.CBoxVendor(cboMaNCC);
            Intercept();
        }

        void Intercept()
        {
            try
            {
                DataGridView.DataSource = BLHang.GetItems();
                DataGridView.AutoResizeColumns();

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = !true;
                btnHuy.Enabled = !true;

                txtMahang.ResetText();
                txtTenhang.ResetText();
                txtSoluong.ResetText();
                txtDongianhap.ResetText();
                txtDongiaban.ResetText();
                txtAnh.ResetText();
                txtGhichu.ResetText();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table Khách Hàng. Lỗi rồi!!!");
            }
        }      

        private void btnReload_Click(object sender, EventArgs e)
        {
            Intercept();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            btnThem.Enabled = !true;
            btnSua.Enabled = !true;
            btnXoa.Enabled = !true;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            txtMahang.ResetText();
            txtTenhang.ResetText();
            txtSoluong.ResetText();
            txtDongianhap.ResetText();
            txtDongiaban.ResetText();
            txtAnh.ResetText();
            txtGhichu.ResetText();

            picAnh.Image = null;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (traloi == DialogResult.Yes)
            {
                if (BLHang.DeleteProduct(txtMahang.Text, ref errr) == true)
                {
                    Intercept();
                    MessageBox.Show("Xoá - Hoàn tất!");
                }
                else
                {
                    MessageBox.Show("Không xoá được. Lỗi rồi!" + Environment.NewLine
                       + errr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ngừng việc xóa mẫu tin!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = !true;
            btnThem.Enabled = !true;
            btnSua.Enabled = !true;
            btnXoa.Enabled = !true;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if (BLHang.InsertProduct(txtTenhang.Text, Convert.ToInt16(cboMaNCC.SelectedValue), 
                    Convert.ToDouble(txtSoluong.Text), Convert.ToDouble(txtDongianhap.Text), txtAnh.Text, txtGhichu.Text
                    , ref errr) == true)
                {
                    Intercept();
                    MessageBox.Show("Đã thêm xong!");
                }
                else
                {
                    MessageBox.Show(errr, "ERROR:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (BLHang.UpdateProduct(txtMahang.Text, txtTenhang.Text, Convert.ToInt16(cboMaNCC.SelectedValue),
                    Convert.ToDouble(txtSoluong.Text), Convert.ToDouble(txtDongianhap.Text), txtAnh.Text, txtGhichu.Text
                    , ref errr) == true)
                {
                    Intercept();
                    MessageBox.Show("Đã sửa xong!");
                }
                else
                {
                    MessageBox.Show(errr, "ERROR:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = !true;
            btnHuy.Enabled = !true;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = !true;
            btnHuy.Enabled = !true;

            DataGridView.DataSource = BLHang.SearchItems(txtTenhang.Text,
                cboMaNCC.SelectedValue.ToString(), txtSoluong.Text, txtDongiaban.Text, txtGhichu.Text);
            DataGridView.AutoResizeColumns();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = DataGridView.CurrentCell.RowIndex;
            if (r < 0 || r >= DataGridView.RowCount - 1) return;
            txtMahang.Text = DataGridView.Rows[r].Cells[0].Value.ToString();
            txtTenhang.Text = DataGridView.Rows[r].Cells[1].Value.ToString();
            cboMaNCC.Text = DataGridView.Rows[r].Cells[2].Value.ToString();
            txtSoluong.Text = DataGridView.Rows[r].Cells[3].Value.ToString();
            txtDongianhap.Text = DataGridView.Rows[r].Cells[4].Value.ToString();
            txtDongiaban.Text = DataGridView.Rows[r].Cells[5].Value.ToString();
            txtGhichu.Text = DataGridView.Rows[r].Cells[6].Value.ToString();
            txtAnh.Text = DataGridView.Rows[r].Cells[7].Value.ToString();
            try
            {
                picAnh.Image = Image.FromFile(txtAnh.Text);
            }
            catch (Exception)
            { MessageBox.Show("Đường dẫn hình ảnh của sản phẩm này không hợp lệ[lỗi:-2]", "THông Báo"); }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }
    }
}
