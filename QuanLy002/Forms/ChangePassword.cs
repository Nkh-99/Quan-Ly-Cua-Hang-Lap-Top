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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            lbRank.Text = FrmLogin.Level.ToString();
            lbId.Text = FrmLogin.MaNV;
            lbName.Text = BLAcount.GetUserByEmployeeId(FrmLogin.MaNV);
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            if (txtOldPass.Text.Equals(FrmLogin.Password))
            {
                if (txtNewPass1.Text.Equals(txtNewPass2.Text) && !txtNewPass1.Text.Equals(txtOldPass.Text))
                {
                    BLAcount.UpdatePassword(lbName.Text, txtNewPass2.Text);
                    MessageBox.Show("Your password has been Changed", "---", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
        }
    }
}
