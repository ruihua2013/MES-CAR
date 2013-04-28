namespace NpMis
{
    using CrystalDecisions.Windows.Forms;
    using NpMis.Report;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_SendList : Form
    {
        private IContainer components;
        private CrystalReportViewer crv_Send;
        private string m_SendId;

        public Frm_SendList()
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

        private void Frm_SendList_Load(object sender, EventArgs e)
        {
            CpSendList list = new CpSendList();
            Invoice invoice = new Invoice();
            if (this.m_SendId.Trim() != "")
            {
                DataSet set = invoice.PrintSendList(this.m_SendId);
                list.SetDataSource(set);
                this.crv_Send.ReportSource=list;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Frm_SendList));
            this.crv_Send = new CrystalReportViewer();
            base.SuspendLayout();
            this.crv_Send.ActiveViewIndex=-1;
            this.crv_Send.BorderStyle = BorderStyle.FixedSingle;
            this.crv_Send.Dock = DockStyle.Fill;
            this.crv_Send.Location = new Point(0, 0);
            this.crv_Send.Name = "crv_Send";
            this.crv_Send.SelectionFormula="";
            this.crv_Send.Size = new Size(0x2b7, 0x1ac);
            this.crv_Send.TabIndex = 1;
            this.crv_Send.ViewTimeSelectionFormula="";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b7, 0x1ac);
            base.Controls.Add(this.crv_Send);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "Frm_SendList";
            this.Text = "送货单";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.Frm_SendList_Load);
            base.ResumeLayout(false);
        }

        public void PrintSendList()
        {
            try
            {
                CpSendList list = new CpSendList();
                Invoice invoice = new Invoice();
                if (this.m_SendId.Trim() != "")
                {
                    DataSet set = invoice.PrintSendList(this.m_SendId);
                    list.SetDataSource(set);
                }
                list.PrintOptions.PrinterName=Invoice.GetSendPrinter();
                list.PrintToPrinter(1, true, 1, 0x1869f);
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印送货单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public string SendId
        {
            get
            {
                return this.m_SendId;
            }
            set
            {
                this.m_SendId = value;
            }
        }
    }
}

