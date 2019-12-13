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
    public partial class FrmKH : Form
    {
        string errr;
        bool Them;
        public FrmKH()
        {
            InitializeComponent();
            txtMakhach.Enabled = false;
        }

        void Intercept()
        {
            try
            {
                dataGridView1.DataSource = BLKhachHang.GetCustomer();
                dataGridView1.AutoResizeColumns();

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;

                //groupBox1.Enabled = false;
                txtTenkhach.Enabled = false;
                txtDiachi.Enabled = false;
                mskDienthoai.Enabled = false;

                txtMakhach.ResetText();
                txtTenkhach.ResetText();
                txtDiachi.ResetText();
                mskDienthoai.ResetText();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table Khách Hàng. Lỗi rồi!!!");
            }
        }

        private void FrmKH_Load(object sender, EventArgs e)
        {
            Intercept();
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
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;

            txtTenkhach.Enabled = !false;
            txtDiachi.Enabled = !false;
            mskDienthoai.Enabled = !false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnHuy.Enabled = true;
            btnLuu.Enabled = true;

            txtTenkhach.Enabled = !false;
            txtDiachi.Enabled = !false;
            mskDienthoai.Enabled = !false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;

            txtTenkhach.Enabled = false;
            txtDiachi.Enabled = false;
            mskDienthoai.Enabled = false;

            txtMakhach.ResetText();
            txtTenkhach.ResetText();
            txtDiachi.ResetText();
            mskDienthoai.ResetText();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if (BLKhachHang.InsertKH(txtTenkhach.Text, txtDiachi.Text, mskDienthoai.Text, ref errr) == true)
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
                if (BLKhachHang.UpdateKH(txtMakhach.Text , txtTenkhach.Text, txtDiachi.Text, mskDienthoai.Text, ref errr) == true)
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (traloi == DialogResult.Yes)
            {
                if (BLKhachHang.DeleteKH(txtMakhach.Text, ref errr) == true)
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

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;

            dataGridView1.DataSource = BLKhachHang.SearchCustomer(txtTenkhach.Text, txtDiachi.Text, mskDienthoai.Text);
            dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            if (r < 0 || r >= dataGridView1.RowCount - 1) return;
            txtMakhach.Text = dataGridView1.Rows[r].Cells[0].Value.ToString();
            txtTenkhach.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
            txtDiachi.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            mskDienthoai.Text = dataGridView1.Rows[r].Cells[3].Value.ToString();
        }
    }
}
