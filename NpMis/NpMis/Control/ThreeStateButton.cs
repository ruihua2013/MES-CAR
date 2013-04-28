namespace NpMis.Control
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ThreeStateButton : UserControl
    {
        private IContainer components;
        private Image ImgDisable;
        private Image ImgDown;
        private Image ImgNoraml;
        private Image ImgOver;
        private bool m_Enable;
        private PictureBox picImage;

        [Browsable(true)]
        public event EventHanderMyClick MyClick;

        public ThreeStateButton()
        {
            this.InitializeComponent();
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
            this.picImage = new PictureBox();
            ((ISupportInitialize) this.picImage).BeginInit();
            base.SuspendLayout();
            this.picImage.Location = new Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new Size(0x30, 0x30);
            this.picImage.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            this.picImage.MouseLeave += new EventHandler(this.picImage_MouseLeave);
            this.picImage.Click += new EventHandler(this.picImage_Click);
            this.picImage.MouseDown += new MouseEventHandler(this.picImage_MouseDown);
            this.picImage.MouseUp += new MouseEventHandler(this.picImage_MouseUp);
            this.picImage.MouseEnter += new EventHandler(this.picImage_MouseEnter);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.picImage);
            base.Name = "ThreeStateButton";
            base.Size = new Size(0x33, 0x33);
            base.EnabledChanged += new EventHandler(this.ThreeStateButton_EnabledChanged);
            ((ISupportInitialize) this.picImage).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            if (this.MyClick != null)
            {
                this.MyClick();
            }
        }

        private void picImage_MouseDown(object sender, MouseEventArgs e)
        {
            this.picImage.Image = this.ImgDown;
        }

        private void picImage_MouseEnter(object sender, EventArgs e)
        {
            this.picImage.Image = this.ImgOver;
        }

        private void picImage_MouseLeave(object sender, EventArgs e)
        {
            this.picImage.Image = this.ImgNoraml;
        }

        private void picImage_MouseUp(object sender, MouseEventArgs e)
        {
            this.picImage.Image = this.ImgOver;
        }

        private void ThreeStateButton_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                this.picImage.Image = this.ImgDisable;
            }
            else
            {
                this.picImage.Image = this.ImgNoraml;
            }
        }

        private void ThreeStateButton_Load(object sender, EventArgs e)
        {
            this.picImage.Location = new Point(0, 0);
        }

        public bool Enabled
        {
            get
            {
                return this.m_Enable;
            }
            set
            {
                this.m_Enable = value;
                if (!this.m_Enable)
                {
                    this.picImage.Image = this.ImgDisable;
                }
                this.picImage.Enabled = value;
            }
        }

        [Browsable(true), Description("无效时显示的图片"), DefaultValue((string) null), Category("Appearance")]
        public Image PicDisable
        {
            get
            {
                return this.ImgDisable;
            }
            set
            {
                this.ImgDisable = value;
            }
        }

        [DefaultValue((string) null), Description("鼠标按下时显示的图片"), Category("Appearance"), Browsable(true)]
        public Image PicDown
        {
            get
            {
                return this.ImgDown;
            }
            set
            {
                this.ImgDown = value;
            }
        }

        [Category("Appearance"), Browsable(true), DefaultValue((string) null), Description("正常时显示的图片")]
        public Image PicNormal
        {
            get
            {
                return this.ImgNoraml;
            }
            set
            {
                this.ImgNoraml = value;
                this.picImage.Image = this.ImgNoraml;
                base.Size = this.picImage.Size;
            }
        }

        [Browsable(true), Description("鼠标经过时显示的图片"), Category("Appearance"), DefaultValue((string) null)]
        public Image PicOver
        {
            get
            {
                return this.ImgOver;
            }
            set
            {
                this.ImgOver = value;
            }
        }
    }
}

