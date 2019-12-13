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
    public partial class FrmNhanVien : Form
    {
        string errr;
        bool Them;
        public FrmNhanVien()
        {
            InitializeComponent();
            txtManhanvien.Enabled = false;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            Intercept();
        }
        void Intercept()
        {
            try
            {
                DataGridView.DataSource = BLNhanVien.GetEmployee();
                DataGridView.AutoResizeColumns();

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = !true;
                btnHuy.Enabled = !true;

                txtManhanvien.ResetText();
                txtTennhanvien.ResetText();
                txtDiachi.ResetText();
                mskDienthoai.ResetText();
                chkGioitinh.CheckState = CheckState.Checked;
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

            txtManhanvien.ResetText();
            txtTennhanvien.ResetText();
            txtDiachi.ResetText();
            mskDienthoai.ResetText();
            chkGioitinh.CheckState = CheckState.Checked;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (traloi == DialogResult.Yes)
            {
                if (BLNhanVien.DeleteEmployee(txtManhanvien.Text, ref errr) == true)
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if (BLNhanVien.InsertEmployee(txtTennhanvien.Text, dtpNV.Value, txtDiachi.Text, mskDienthoai.Text, chkGioitinh.Checked, ref errr) == true)
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
                if (BLNhanVien.UpdateEmployee(txtManhanvien.Text , txtTennhanvien.Text, dtpNV.Value, txtDiachi.Text, mskDienthoai.Text, chkGioitinh.Checked, ref errr) == true)
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

            string gt = "Men";
            if (chkGioitinh.CheckState == CheckState.Unchecked)
                gt = "Women";
            DataGridView.DataSource = BLNhanVien.SearchEmployee(txtTennhanvien.Text, gt, txtDiachi.Text, mskDienthoai.Text);
            DataGridView.AutoResizeColumns();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = DataGridView.CurrentCell.RowIndex;
            if (r < 0 || r >= DataGridView.RowCount - 1) return;
            txtManhanvien.Text = DataGridView.Rows[r].Cells[0].Value.ToString();
            txtTennhanvien.Text = DataGridView.Rows[r].Cells[1].Value.ToString();
            string Men = DataGridView.Rows[r].Cells[2].Value.ToString();
            if (Men.StartsWith("M"))
                chkGioitinh.CheckState = CheckState.Checked;

            mskDienthoai.Text = DataGridView.Rows[r].Cells[4].Value.ToString();
            txtDiachi.Text = DataGridView.Rows[r].Cells[5].Value.ToString();
            dtpNV.Value = Convert.ToDateTime(DataGridView.Rows[r].Cells[3].Value);
        }
    }
}
