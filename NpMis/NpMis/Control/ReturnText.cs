namespace NpMis.Control
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ReturnText : TextBox
    {
        private IContainer components;
        private Button m_LinkButton;

        public ReturnText()
        {
            this.InitializeComponent();
            base.KeyPress += new KeyPressEventHandler(this.ReturnDown);
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
            this.components = new Container();
        }

        private void ReturnDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.m_LinkButton.PerformClick();
            }
        }

        [Description("回车关联的按钮")]
        public Button LickButton
        {
            get
            {
                return this.m_LinkButton;
            }
            set
            {
                this.m_LinkButton = value;
            }
        }
    }
}

