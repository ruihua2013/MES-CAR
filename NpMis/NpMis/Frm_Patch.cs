namespace NpMis
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_Patch : Form
    {
        private Button btn_Ok;
        private Button btn_PrintBarCode;
        private CheckBox cbx_SelectAll;
        private ComboBox cbx_Size;
        private ComboBox cbx_WProcedure;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader22;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private SortedListView Lvw_License;
        private SortedListView Lvw_Task;
        private static Frm_Patch m_Instance;
        private Panel panel1;
        private TextBox tbx_Amount;

        private Frm_Patch()
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

        private void Frm_Patch_Load(object sender, EventArgs e)
        {
            base.Size = base.Parent.ClientSize;
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.groupBox1 = new GroupBox();
            this.panel1 = new Panel();
            this.tbx_Amount = new TextBox();
            this.cbx_SelectAll = new CheckBox();
            this.btn_PrintBarCode = new Button();
            this.btn_Ok = new Button();
            this.label3 = new Label();
            this.cbx_WProcedure = new ComboBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cbx_Size = new ComboBox();
            this.Lvw_Task = new SortedListView();
            this.columnHeader21 = new ColumnHeader();
            this.columnHeader22 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columnHeader23 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.Lvw_License = new SortedListView();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader13 = new ColumnHeader();
            this.columnHeader14 = new ColumnHeader();
            this.columnHeader15 = new ColumnHeader();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.Lvw_Task);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(8, 0x143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(5);
            this.groupBox2.Size = new Size(0x2d8, 0x8a);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "今天下达的任务";
            this.groupBox1.Controls.Add(this.Lvw_License);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(5);
            this.groupBox1.Size = new Size(0x2d8, 0x13b);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下达补作生产任务";
            this.panel1.Controls.Add(this.tbx_Amount);
            this.panel1.Controls.Add(this.cbx_SelectAll);
            this.panel1.Controls.Add(this.btn_PrintBarCode);
            this.panel1.Controls.Add(this.btn_Ok);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbx_WProcedure);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbx_Size);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(5, 0x13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2ce, 0x72);
            this.panel1.TabIndex = 0;
            this.tbx_Amount.Font = new Font("黑体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.tbx_Amount.Location = new Point(0x113, 0x12);
            this.tbx_Amount.Name = "tbx_Amount";
            this.tbx_Amount.Size = new Size(0x6b, 0x17);
            this.tbx_Amount.TabIndex = 0x1a;
            this.tbx_Amount.Text = "鲁";
            this.cbx_SelectAll.AutoSize = true;
            this.cbx_SelectAll.Checked = true;
            this.cbx_SelectAll.CheckState = CheckState.Checked;
            this.cbx_SelectAll.Location = new Point(0x11, 70);
            this.cbx_SelectAll.Name = "cbx_SelectAll";
            this.cbx_SelectAll.Size = new Size(0x4e, 0x10);
            this.cbx_SelectAll.TabIndex = 0x1d;
            this.cbx_SelectAll.Text = "全选/不选";
            this.cbx_SelectAll.UseVisualStyleBackColor = true;
            this.btn_PrintBarCode.Location = new Point(0x248, 0x11);
            this.btn_PrintBarCode.Name = "btn_PrintBarCode";
            this.btn_PrintBarCode.Size = new Size(0x4b, 0x17);
            this.btn_PrintBarCode.TabIndex = 0x1c;
            this.btn_PrintBarCode.Text = "添加到任务";
            this.btn_PrintBarCode.UseVisualStyleBackColor = true;
            this.btn_Ok.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Ok.ForeColor = Color.FromArgb(0, 0xc0, 0);
            this.btn_Ok.Location = new Point(0x248, 0x3f);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new Size(0x4b, 0x17);
            this.btn_Ok.TabIndex = 0x1b;
            this.btn_Ok.Text = "下达任务";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(210, 0x17);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 0x19;
            this.label3.Text = "车牌号码:";
            this.cbx_WProcedure.FormattingEnabled = true;
            this.cbx_WProcedure.Location = new Point(0x1ce, 20);
            this.cbx_WProcedure.Name = "cbx_WProcedure";
            this.cbx_WProcedure.Size = new Size(0x5d, 20);
            this.cbx_WProcedure.TabIndex = 0x18;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x199, 0x17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2f, 12);
            this.label2.TabIndex = 0x17;
            this.label2.Text = "选择片:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 0x16;
            this.label1.Text = "车牌类型:";
            this.cbx_Size.FormattingEnabled = true;
            this.cbx_Size.Location = new Point(80, 20);
            this.cbx_Size.Name = "cbx_Size";
            this.cbx_Size.Size = new Size(0x60, 20);
            this.cbx_Size.TabIndex = 0x15;
            this.Lvw_Task.CheckBoxes = true;
            this.Lvw_Task.Columns.AddRange(new ColumnHeader[] { this.columnHeader21, this.columnHeader22, this.columnHeader12, this.columnHeader23, this.columnHeader4, this.columnHeader5 });
            this.Lvw_Task.Dock = DockStyle.Fill;
            this.Lvw_Task.FullRowSelect = true;
            this.Lvw_Task.Location = new Point(5, 0x13);
            this.Lvw_Task.Name = "Lvw_Task";
            this.Lvw_Task.Size = new Size(0x2ce, 0x72);
            this.Lvw_Task.TabIndex = 2;
            this.Lvw_Task.UseCompatibleStateImageBehavior = false;
            this.Lvw_Task.View = View.Details;
            this.columnHeader21.Text = "生产任务单号";
            this.columnHeader21.Width = 130;
            this.columnHeader22.Text = "下达时间";
            this.columnHeader22.Width = 130;
            this.columnHeader12.Text = "车牌总数";
            this.columnHeader12.Width = 80;
            this.columnHeader23.Text = "下达人员";
            this.columnHeader23.Width = 80;
            this.columnHeader4.Text = "装箱时间";
            this.columnHeader4.Width = 130;
            this.columnHeader5.Text = "装箱人员";
            this.columnHeader5.Width = 80;
            this.Lvw_License.CheckBoxes = true;
            this.Lvw_License.Columns.AddRange(new ColumnHeader[] { this.columnHeader11, this.columnHeader1, this.columnHeader13, this.columnHeader14, this.columnHeader15 });
            this.Lvw_License.Dock = DockStyle.Fill;
            this.Lvw_License.FullRowSelect = true;
            this.Lvw_License.Location = new Point(5, 0x85);
            this.Lvw_License.Name = "Lvw_License";
            this.Lvw_License.Size = new Size(0x2ce, 0xb1);
            this.Lvw_License.TabIndex = 13;
            this.Lvw_License.UseCompatibleStateImageBehavior = false;
            this.Lvw_License.View = View.Details;
            this.columnHeader11.Text = "车牌号码";
            this.columnHeader11.Width = 0x7e;
            this.columnHeader1.Text = "车牌种类";
            this.columnHeader1.Width = 120;
            this.columnHeader13.Text = "下达时间";
            this.columnHeader13.Width = 0x88;
            this.columnHeader14.Text = "最后完成期限";
            this.columnHeader14.Width = 0x87;
            this.columnHeader15.Text = "计划单号";
            this.columnHeader15.Width = 0x77;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2e8, 0x1d5);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_Patch";
            base.Padding = new Padding(8);
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Frm_Patch";
            base.Load += new EventHandler(this.Frm_Patch_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        public static Frm_Patch Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Patch();
                }
                return m_Instance;
            }
        }
    }
}

