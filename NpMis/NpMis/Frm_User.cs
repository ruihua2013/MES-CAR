namespace NpMis
{
    using NpMis.Control;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_User : Form
    {
        private Button button1;
        private IContainer components;
        private GroupBox groupBox1;
        private ReturnText returnText1;

        public Frm_User()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("你引发了按钮单击事件");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.groupBox1 = new GroupBox();
            this.returnText1 = new ReturnText();
            base.SuspendLayout();
            this.button1.Location = new Point(0x156, 0x4c);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.groupBox1.Location = new Point(0x79, 0x2c);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe9, 0xc4);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.returnText1.LickButton = this.button1;
            this.returnText1.Location = new Point(0x27, 0x6f);
            this.returnText1.Name = "returnText1";
            this.returnText1.Size = new Size(100, 0x15);
            this.returnText1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x20b, 440);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.returnText1);
            base.Name = "Frm_User";
            this.Text = "用户管理";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void threeStateButton1_Click(object sender, EventArgs e)
        {
        }

        private void threeStateButton1_Load(object sender, EventArgs e)
        {
        }

        private void threeStateButton1_MyClick()
        {
            MessageBox.Show("Test");
        }
    }
}

