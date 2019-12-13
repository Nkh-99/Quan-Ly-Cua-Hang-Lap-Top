namespace QuanLy002.Forms
{
    partial class ChangePassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbRank = new System.Windows.Forms.Label();
            this.lbId = new System.Windows.Forms.Label();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.txtNewPass1 = new System.Windows.Forms.TextBox();
            this.txtNewPass2 = new System.Windows.Forms.TextBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rank:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "EMployee Id:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.BackColor = System.Drawing.Color.Transparent;
            this.lbName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(111, 26);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(46, 17);
            this.lbName.TabIndex = 6;
            this.lbName.Text = "name";
            // 
            // lbRank
            // 
            this.lbRank.AutoSize = true;
            this.lbRank.BackColor = System.Drawing.Color.Transparent;
            this.lbRank.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRank.Location = new System.Drawing.Point(111, 55);
            this.lbRank.Name = "lbRank";
            this.lbRank.Size = new System.Drawing.Size(36, 17);
            this.lbRank.TabIndex = 7;
            this.lbRank.Text = "rank";
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.BackColor = System.Drawing.Color.Transparent;
            this.lbId.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbId.Location = new System.Drawing.Point(111, 89);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(20, 17);
            this.lbId.TabIndex = 8;
            this.lbId.Text = "id";
            // 
            // txtOldPass
            // 
            this.txtOldPass.Location = new System.Drawing.Point(15, 128);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Size = new System.Drawing.Size(190, 20);
            this.txtOldPass.TabIndex = 9;
            this.txtOldPass.Text = "old pass";
            // 
            // txtNewPass1
            // 
            this.txtNewPass1.Location = new System.Drawing.Point(15, 154);
            this.txtNewPass1.Name = "txtNewPass1";
            this.txtNewPass1.Size = new System.Drawing.Size(190, 20);
            this.txtNewPass1.TabIndex = 10;
            this.txtNewPass1.Text = "input new pass here";
            // 
            // txtNewPass2
            // 
            this.txtNewPass2.Location = new System.Drawing.Point(15, 180);
            this.txtNewPass2.Name = "txtNewPass2";
            this.txtNewPass2.Size = new System.Drawing.Size(190, 20);
            this.txtNewPass2.TabIndex = 11;
            this.txtNewPass2.Text = "input new pass here";
            // 
            // btnExec
            // 
            this.btnExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExec.ForeColor = System.Drawing.Color.Tomato;
            this.btnExec.Location = new System.Drawing.Point(51, 226);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(117, 23);
            this.btnExec.TabIndex = 12;
            this.btnExec.Text = "OKe";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 261);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.txtNewPass2);
            this.Controls.Add(this.txtNewPass1);
            this.Controls.Add(this.txtOldPass);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.lbRank);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChangePassword";
            this.Text = "ChangePassword";
            this.Load += new System.EventHandler(this.ChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbRank;
        private System.Windows.Forms.Label lbId;
        private System.Windows.Forms.TextBox txtOldPass;
        private System.Windows.Forms.TextBox txtNewPass1;
        private System.Windows.Forms.TextBox txtNewPass2;
        private System.Windows.Forms.Button btnExec;
    }
}