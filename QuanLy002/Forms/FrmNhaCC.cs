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
    public partial class FrmNhaCC : Form
    {
        string errr;
        bool Them;
        public FrmNhaCC()
        {
            InitializeComponent();
            TxtMaNCC.Enabled = false;
        }

        private void FrmNhaCC_Load(object sender, EventArgs e)
        {
            Intercept();
        }

        void Intercept()
        {
            try {               

                dataGridView1.DataSource = BLNhaCC.GetNhaCC();
                
                dataGridView1.AutoResizeColumns();
            } 
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message, "ERROR:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnSearch.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            TxtTenNCC.Enabled = false;
            TxtMaNCC.ResetText();
            TxtTenNCC.ResetText();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            TxtTenNCC.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            TxtMaNCC.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            TxtTenNCC.Enabled = true;
            TxtTenNCC.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;

            TxtMaNCC.ResetText();
            TxtTenNCC.Enabled = false;
            TxtTenNCC.ResetText();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if (BLNhaCC.InsertNCC(TxtTenNCC.Text, ref errr) == true)
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
                int ID = Convert.ToInt32(TxtMaNCC.Text);
                if (BLNhaCC.UpdateNCC(ID, TxtTenNCC.Text, ref errr) == true)
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
                int ID = Convert.ToInt32(TxtMaNCC.Text);
                if (BLNhaCC.DeleteNCC(ID, ref errr) == true)
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            if (r < 0 || r >= dataGridView1.RowCount - 1) return;
            TxtMaNCC.Text = dataGridView1.Rows[r].Cells[0].Value.ToString();
            TxtTenNCC.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnSearch.Enabled = true;

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;

            dataGridView1.DataSource = BLNhaCC.SearchVendor(TxtTenNCC.Text);
            dataGridView1.AutoResizeColumns();
        }
    }
   
}
