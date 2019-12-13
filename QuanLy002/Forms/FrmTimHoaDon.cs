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
    public partial class FrmTimHoaDon : Form
    {
        public FrmTimHoaDon()
        {
            InitializeComponent();
        }

        private void FrmTimHoaDon_Load(object sender, EventArgs e)
        {
            ResetValues();
            DataGridView.DataSource = null;
        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHDBan.Focus();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if ((txtMaHDBan.Text != "") || (txtThang.Text != "") || (txtNam.Text != "") ||
                (txtTongtien.Text != "") ||
               (txtManhanvien.Text != "") || (txtMakhach.Text != ""))
            {
                try
                {

                    DataTable table = BLHoaDon.SearchInvoice(txtMaHDBan.Text, txtManhanvien.Text, txtMakhach.Text,
                        txtThang.Text, txtNam.Text, txtTongtien.Text);

                    if (table.Rows.Count == 0)
                        MessageBox.Show("Không có hóa đơn thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Có " + table.Rows.Count + " hóa đơn thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridView.DataSource = table;
                    DataGridView.AutoResizeColumns();
                }
                catch
                {
                    MessageBox.Show("Lỗi khi đang truy xuất hoá đơn cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTongtien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (DataGridView.RowCount <= 0) return;
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = DataGridView.CurrentRow.Cells["Invoice ID"].Value.ToString();
                FrmHoaDon frm = new FrmHoaDon();
                frm.txtMaHDBan.Text = mahd;
                frm.ShowDialog();
            }
        }
    }
}
