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

namespace QuanLy002
{
    public partial class FrmLogin : Form
    {
        public static int Level;
        public static string MaNV;
        public static string Password;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (BLAcount.CheckAccount(TxtUser.Text, TxtPasw.Text, ref Level, MaNV: ref MaNV))
            {
                MessageBox.Show("login successfully!");
                Password = TxtPasw.Text;
                FrmMain Main = new FrmMain();
                Main.Show();
                this.Hide();
            }
            else
            {
                DialogResult ds = MessageBox.Show("login failed!", "Thông báo đăng nhập thất bại", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (ds == DialogResult.OK)
                {
                    TxtUser.ResetText(); TxtPasw.ResetText();
                }
                TxtPasw.Focus();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtPasw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnOK.Focus();
            }
        }
    }
}
