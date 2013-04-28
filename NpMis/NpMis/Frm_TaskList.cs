namespace NpMis
{
    using CrystalDecisions.Windows.Forms;
    using NpMis.Report;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_TaskList : Form
    {
        private IContainer components;
        private CrystalReportViewer crv_Task;
        private string m_TaskId;

        public Frm_TaskList()
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

        private void Frm_TaskList_Load(object sender, EventArgs e)
        {
            CpTask task = new CpTask();
            Task task2 = new Task();
            if (this.m_TaskId.Trim() != "")
            {
                DataSet set = task2.PrintTaskList(this.m_TaskId);
                task.SetDataSource(set);
                this.crv_Task.ReportSource=task;
            }
        }

        private void InitializeComponent()
        {
            this.crv_Task = new CrystalReportViewer();
            base.SuspendLayout();
            this.crv_Task.ActiveViewIndex=-1;
            this.crv_Task.BorderStyle = BorderStyle.FixedSingle;
            this.crv_Task.Dock = DockStyle.Fill;
            this.crv_Task.Location = new Point(0, 0);
            this.crv_Task.Name = "crv_Task";
            this.crv_Task.SelectionFormula="";
            this.crv_Task.Size = new Size(0x200, 360);
            this.crv_Task.TabIndex = 0;
            this.crv_Task.ViewTimeSelectionFormula="";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x200, 360);
            base.Controls.Add(this.crv_Task);
            base.Name = "Frm_TaskList";
            base.ShowIcon = false;
            this.Text = "生产任务单打印";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.Frm_TaskList_Load);
            base.ResumeLayout(false);
        }

        public string TaskId
        {
            get
            {
                return this.m_TaskId;
            }
            set
            {
                this.m_TaskId = value;
            }
        }
    }
}

