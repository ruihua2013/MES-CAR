namespace NpMis
{
    using Common;
    using CrystalDecisions.Shared;
    using CrystalDecisions.Windows.Forms;
    using NpMis.Report;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_NpStat : Form
    {
        private IContainer components;
        private CrystalReportViewer crv_Send;
        private DateTime m_FromDate;
        private string m_State;
        private DateTime m_ToDate;
        private string StateDesc;

        public Frm_NpStat()
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

        private void Frm_NpStat_Load(object sender, EventArgs e)
        {
            CpNpStat stat = new CpNpStat();
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet ds = new DataSet();
            access.Run_SqlText(ref ds, "select Description ,TotalNum  from V_NpType t inner join (select Code,Count(NpId) as TotalNum from V_Np Where InPutTime between '" + this.m_FromDate.ToShortDateString() + "' and '" + this.ToDate.AddDays(1.0).ToShortDateString() + "'" + this.m_State + " Group By Code) t1 on t.Code=t1.Code", "V_NpStat");
            ParameterFields fields = new ParameterFields();
            ParameterField field = new ParameterField();
            ParameterDiscreteValue value2 = new ParameterDiscreteValue();
            field.ParameterFieldName="FromDate";
            value2.Value=this.m_FromDate.ToShortDateString();
            field.CurrentValues.Add(value2);
            fields.Add(field);
            field = new ParameterField();
            field.ParameterFieldName="ToDate";
            value2 = new ParameterDiscreteValue();
            value2.Value=this.m_ToDate.ToShortDateString();
            field.CurrentValues.Add(value2);
            fields.Add(field);
            field = new ParameterField();
            field.ParameterFieldName="State";
            value2 = new ParameterDiscreteValue();
            value2.Value=this.StateDesc;
            field.CurrentValues.Add(value2);
            fields.Add(field);
            this.crv_Send.ParameterFieldInfo=fields;
            stat.SetDataSource(ds);
            this.crv_Send.ReportSource=stat;
        }

        private void InitializeComponent()
        {
            this.crv_Send = new CrystalReportViewer();
            base.SuspendLayout();
            this.crv_Send.ActiveViewIndex=-1;
            this.crv_Send.BorderStyle = BorderStyle.FixedSingle;
            this.crv_Send.Dock = DockStyle.Fill;
            this.crv_Send.Location = new Point(0, 0);
            this.crv_Send.Name = "crv_Send";
            this.crv_Send.SelectionFormula="";
            this.crv_Send.Size = new Size(0x29d, 390);
            this.crv_Send.TabIndex = 3;
            this.crv_Send.ViewTimeSelectionFormula="";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x29d, 390);
            base.Controls.Add(this.crv_Send);
            base.Name = "Frm_NpStat";
            base.ShowIcon = false;
            this.Text = "生产量统计";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.Frm_NpStat_Load);
            base.ResumeLayout(false);
        }

        public void PrintSendDetail()
        {
            try
            {
                CpNpStat stat = new CpNpStat();
                Sql2KDataAccess access = new Sql2KDataAccess();
                DataSet ds = new DataSet();
                access.Run_SqlText(ref ds, "select Description ,TotalNum  from V_NpType t inner join (select Code,Count(NpId) as TotalNum from V_Np Where InPutTime between '" + this.m_FromDate.ToShortDateString() + "' and '" + this.ToDate.AddDays(1.0).ToShortDateString() + "'" + this.m_State + " Group By Code) t1 on t.Code=t1.Code", "V_NpStat");
                ParameterFields fields = new ParameterFields();
                ParameterField field = new ParameterField();
                ParameterDiscreteValue value2 = new ParameterDiscreteValue();
                field.ParameterFieldName="FromDate";
                value2.Value=this.m_FromDate.ToShortDateString();
                field.CurrentValues.Add(value2);
                fields.Add(field);
                field = new ParameterField();
                field.ParameterFieldName="ToDate";
                value2 = new ParameterDiscreteValue();
                value2.Value=this.m_ToDate.ToShortDateString();
                field.CurrentValues.Add(value2);
                fields.Add(field);
                field = new ParameterField();
                field.ParameterFieldName="State";
                value2 = new ParameterDiscreteValue();
                value2.Value=this.StateDesc;
                field.CurrentValues.Add(value2);
                fields.Add(field);
                this.crv_Send.ParameterFieldInfo=fields;
                ds.Tables[0].TableName = "V_NpState";
                stat.SetDataSource(ds);
                this.crv_Send.ReportSource=stat;
                this.crv_Send.PrintReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印生产量统计", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public DateTime FromDate
        {
            get
            {
                return this.m_FromDate;
            }
            set
            {
                this.m_FromDate = value;
            }
        }

        public string NpState
        {
            set
            {
                switch (value.Trim())
                {
                    case "未制作":
                        this.m_State = " and TaskId is null";
                        this.StateDesc = value;
                        return;

                    case "制作中":
                        this.m_State = " and TaskId is not null and SendId is null";
                        this.StateDesc = value;
                        return;

                    case "制作完成":
                        this.m_State = " and SendId is not null";
                        this.StateDesc = value;
                        return;
                }
                this.m_State = "";
                this.StateDesc = "全部";
            }
        }

        public DateTime ToDate
        {
            get
            {
                return this.m_ToDate;
            }
            set
            {
                this.m_ToDate = value;
            }
        }
    }
}

