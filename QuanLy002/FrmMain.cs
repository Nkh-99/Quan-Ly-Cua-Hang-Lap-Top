using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy002.Forms;

namespace QuanLy002
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            int rank = FrmLogin.Level;
            LbTaiKhoan.Text += " " + FrmLogin.Level;
            switch (rank)
            {
                case 2:     // nhan vien
                    LbNhanVien.ForeColor = Color.LightSkyBlue;
                    this.MnuNhaCC.Visible = false;
                    this.MnuHanghoa.Visible = false;
                    this.MnuNhanvien.Visible = false;
                    this.mnuTimkiem.Visible = false;
                    this.lậpBáoCáoToolStripMenuItem.Visible = false;
                    break;
                case 3:     // ke toan
                    LbNhanVien.ForeColor = Color.Orange;
                    LbNhanVien.Text = "Ke Toan:";
                    this.mnuHoadon.Visible = false;
                    this.danhMụcToolStripMenuItem.Visible = false;
                    this.lậpBáoCáoToolStripMenuItem.Visible = false;
                    break;
                case 4:     // thu kho
                    LbNhanVien.Text = "Thu Kho:";
                    this.mnuHoadon.Visible = false;
                    this.MnuKhachhang.Visible = false;
                    this.MnuNhanvien.Visible = false;
                    this.mnuTimkiem.Visible = false;
                    this.lậpBáoCáoToolStripMenuItem.Visible = false;
                    break;
                default:
                    LbNhanVien.Text = "Admin:";
                    LbNhanVien.ForeColor = Color.OrangeRed;
                    break;
            }
            LbNhanVien.Text += " " + FrmLogin.MaNV;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MnuDangXuat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã đăng xuất.. Đợi vài giây để có thể đăng nhập lại!");
            Application.Restart();
        }

        private void MnuThoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tạm biệt, hẹn gặp lại");
            Application.Exit();
        }

        private void MnuNhaCC_Click(object sender, EventArgs e)
        {
            var frm = new FrmNhaCC();
            frm.Show();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = new Random().Next(0, 100);            
        }

        private void MnuKhachhang_Click(object sender, EventArgs e)
        {
            var frm = new FrmKH();
            frm.Show();
        }

        private void MnuHanghoa_Click(object sender, EventArgs e)
        {
            var frm = new FrmHang();
            frm.Show();
        }

        private void MnuNhanvien_Click(object sender, EventArgs e)
        {
            var frm = new FrmNhanVien();
            frm.Show();
        }

        private void MnuHDBan_Click(object sender, EventArgs e)
        {
            var frm = new FrmHoaDon();
            frm.Show();
        }

        private void MnuTimHoadon_Click(object sender, EventArgs e)
        {
            var frm = new FrmTimHoaDon();
            frm.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ChangePassword();
            frm.Show();
        }
    }
}
