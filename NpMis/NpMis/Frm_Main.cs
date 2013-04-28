namespace NpMis
{
    using NpMis.Control;
    using PlanInput;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_Main : Form
    {
        private IContainer components;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel pnl_ToolBar;
        private System.Windows.Forms.ToolTip ToolTip;
        private ThreeStateButton tsb_Appr;
        private ThreeStateButton tsb_MTask;
        private ThreeStateButton tsb_Option;
        private ThreeStateButton tsb_Out;
        private ThreeStateButton tsb_Plan;
        private ThreeStateButton tsb_Prod;
        private ThreeStateButton tsb_Query;

        public Frm_Main()
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

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            this.tsb_Plan.Enabled = User.IsHaveAuthority("计划单录入");
            this.tsb_Appr.Enabled = User.IsHaveAuthority("普通审批");
            this.tsb_Prod.Enabled = User.IsHaveAuthority("生产管理");
            this.tsb_Out.Enabled = User.IsHaveAuthority("出库管理");
            this.tsb_Query.Enabled = ((User.IsHaveAuthority("车牌查询") || User.IsHaveAuthority("计划单查询")) || User.IsHaveAuthority("生产任务单查询")) || User.IsHaveAuthority("送货单查询");
            this.tsb_Option.Enabled = ((User.IsHaveAuthority("系统管理") || User.IsHaveAuthority("用户管理")) || User.IsHaveAuthority("权限设定")) || User.IsHaveAuthority("修改密码");
            this.tsb_MTask.Enabled = User.IsHaveAuthority("底板任务管理");
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Frm_Main));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_ToolBar = new Panel();
            this.panel4 = new Panel();
            this.panel3 = new Panel();
            this.tsb_MTask = new ThreeStateButton();
            this.tsb_Option = new ThreeStateButton();
            this.tsb_Query = new ThreeStateButton();
            this.tsb_Out = new ThreeStateButton();
            this.tsb_Plan = new ThreeStateButton();
            this.tsb_Appr = new ThreeStateButton();
            this.tsb_Prod = new ThreeStateButton();
            this.panel2 = new Panel();
            this.pnl_ToolBar.SuspendLayout();
            this.panel3.SuspendLayout();
            base.SuspendLayout();
            this.pnl_ToolBar.Controls.Add(this.panel4);
            this.pnl_ToolBar.Controls.Add(this.panel3);
            this.pnl_ToolBar.Controls.Add(this.panel2);
            this.pnl_ToolBar.Dock = DockStyle.Top;
            this.pnl_ToolBar.Location = new Point(0, 0);
            this.pnl_ToolBar.Name = "pnl_ToolBar";
            this.pnl_ToolBar.Size = new Size(0x404, 0x68);
            this.pnl_ToolBar.TabIndex = 13;
            this.panel4.BackgroundImage = Rs.Middle;
            this.panel4.Dock = DockStyle.Fill;
            this.panel4.Location = new Point(200, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0, 0x68);
            this.panel4.TabIndex = 2;
            this.panel3.BackgroundImage = Rs.Middle;
            this.panel3.Controls.Add(this.tsb_MTask);
            this.panel3.Controls.Add(this.tsb_Option);
            this.panel3.Controls.Add(this.tsb_Query);
            this.panel3.Controls.Add(this.tsb_Out);
            this.panel3.Controls.Add(this.tsb_Plan);
            this.panel3.Controls.Add(this.tsb_Appr);
            this.panel3.Controls.Add(this.tsb_Prod);
            this.panel3.Dock = DockStyle.Right;
            this.panel3.Location = new Point(0x51, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x3b3, 0x68);
            this.panel3.TabIndex = 1;
            this.tsb_MTask.BackColor = SystemColors.Control;
            this.tsb_MTask.Enabled = false;
            this.tsb_MTask.Location = new Point(0x233, 8);
            this.tsb_MTask.Name = "tsb_MTask";
            this.tsb_MTask.PicDisable = Rs.禁用;
            this.tsb_MTask.PicDown = Rs.按下;
            this.tsb_MTask.PicNormal = Rs.正常;
            this.tsb_MTask.PicOver = Rs.经过;
            this.tsb_MTask.Size = new Size(0x5f, 90);
            this.tsb_MTask.TabIndex = 30;
            this.tsb_MTask.MyClick += new EventHanderMyClick(this.tsb_MTask_MyClick);
            this.tsb_Option.BackColor = SystemColors.Control;
            this.tsb_Option.Enabled = false;
            this.tsb_Option.Location = new Point(0x32b, 8);
            this.tsb_Option.Name = "tsb_Option";
            this.tsb_Option.PicDisable = Rs.系统统计不可用;
            this.tsb_Option.PicDown = Rs.系统统计按下;
            this.tsb_Option.PicNormal = Rs.系统统计正常;
            this.tsb_Option.PicOver = Rs.系统统计经过;
            this.tsb_Option.Size = new Size(0x5f, 90);
            this.tsb_Option.TabIndex = 0x1d;
            this.tsb_Option.MyClick += new EventHanderMyClick(this.tsb_Option_MyClick);
            this.tsb_Query.BackColor = SystemColors.Control;
            this.tsb_Query.Enabled = false;
            this.tsb_Query.Location = new Point(0x2af, 8);
            this.tsb_Query.Name = "tsb_Query";
            this.tsb_Query.PicDisable = Rs.查询统计不可用;
            this.tsb_Query.PicDown = Rs.查询统计按下;
            this.tsb_Query.PicNormal = Rs.查询统计正常;
            this.tsb_Query.PicOver = Rs.查询统计经过;
            this.tsb_Query.Size = new Size(0x5f, 90);
            this.tsb_Query.TabIndex = 0x1c;
            this.tsb_Query.MyClick += new EventHanderMyClick(this.tsb_Query_MyClick);
            this.tsb_Out.BackColor = SystemColors.Control;
            this.tsb_Out.Enabled = false;
            this.tsb_Out.Location = new Point(0x1b7, 8);
            this.tsb_Out.Name = "tsb_Out";
            this.tsb_Out.PicDisable = Rs.出库管理不可用;
            this.tsb_Out.PicDown = Rs.出库管理按下;
            this.tsb_Out.PicNormal = Rs.出库管理正常;
            this.tsb_Out.PicOver = Rs.出库管理经过;
            this.tsb_Out.Size = new Size(0x5f, 90);
            this.tsb_Out.TabIndex = 0x1b;
            this.tsb_Out.MyClick += new EventHanderMyClick(this.tsb_Out_MyClick);
            this.tsb_Plan.BackColor = SystemColors.Control;
            this.tsb_Plan.Enabled = false;
            this.tsb_Plan.Location = new Point(0x43, 8);
            this.tsb_Plan.Name = "tsb_Plan";
            this.tsb_Plan.PicDisable = Rs.计划管理不可用;
            this.tsb_Plan.PicDown = Rs.计划管理按下;
            this.tsb_Plan.PicNormal = Rs.计划管理正常;
            this.tsb_Plan.PicOver = Rs.计划管理经过;
            this.tsb_Plan.Size = new Size(0x5f, 90);
            this.tsb_Plan.TabIndex = 0x18;
            this.tsb_Plan.MyClick += new EventHanderMyClick(this.tsb_Plan_MyClick);
            this.tsb_Appr.BackColor = SystemColors.Control;
            this.tsb_Appr.Enabled = false;
            this.tsb_Appr.Location = new Point(0xbf, 8);
            this.tsb_Appr.Name = "tsb_Appr";
            this.tsb_Appr.PicDisable = Rs.不可用;
            this.tsb_Appr.PicDown = Rs._3按下;
            this.tsb_Appr.PicNormal = Rs._1正常;
            this.tsb_Appr.PicOver = Rs._2经过;
            this.tsb_Appr.Size = new Size(0x5f, 90);
            this.tsb_Appr.TabIndex = 0x1a;
            this.tsb_Appr.Load += new EventHandler(this.tsb_Appr_Load);
            this.tsb_Appr.MyClick += new EventHanderMyClick(this.tsb_Appr_MyClick);
            this.tsb_Prod.BackColor = SystemColors.Control;
            this.tsb_Prod.Enabled = false;
            this.tsb_Prod.Location = new Point(0x13b, 8);
            this.tsb_Prod.Name = "tsb_Prod";
            this.tsb_Prod.PicDisable = Rs.生产管理不可用;
            this.tsb_Prod.PicDown = (Image) manager.GetObject("tsb_Prod.PicDown");
            this.tsb_Prod.PicNormal = (Image) manager.GetObject("tsb_Prod.PicNormal");
            this.tsb_Prod.PicOver = (Image) manager.GetObject("tsb_Prod.PicOver");
            this.tsb_Prod.Size = new Size(0x5f, 90);
            this.tsb_Prod.TabIndex = 0x19;
            this.tsb_Prod.MyClick += new EventHanderMyClick(this.tsb_Prod_MyClick);
            this.panel2.BackgroundImage = Rs.Left;
            this.panel2.Dock = DockStyle.Left;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(200, 0x68);
            this.panel2.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = Rs.底图;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x404, 0x227);
            base.Controls.Add(this.pnl_ToolBar);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.IsMdiContainer = true;
            base.Name = "Frm_Main";
            this.Text = "通产车牌生产管理系统";
            base.WindowState = FormWindowState.Maximized;
            base.FormClosing += new FormClosingEventHandler(this.Frm_Main_FormClosing);
            base.Load += new EventHandler(this.Frm_Main_Load);
            this.pnl_ToolBar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void tbs_Patch_MyClick()
        {
            Frm_Patch.Instance.MdiParent = this;
            Frm_Patch.Instance.Show();
            Frm_Patch.Instance.Focus();
        }

        private void tsb_Appr_Load(object sender, EventArgs e)
        {
        }

        private void tsb_Appr_MyClick()
        {
            Frm_Appr.Instance.MdiParent = this;
            Frm_Appr.G_IsRemarkAppr = false;
            Frm_Appr.Instance.Show();
            Frm_Appr.Instance.Focus();
        }

        private void tsb_MTask_MyClick()
        {
            Frm_MatherBoard.Instance.MdiParent = this;
            Frm_MatherBoard.Instance.Show();
            Frm_MatherBoard.Instance.Focus();
        }

        private void tsb_Option_Click(object sender, EventArgs e)
        {
        }

        private void tsb_Option_MyClick()
        {
            Frm_Sys.Instance.ShowDialog();
        }

        private void tsb_Out_MyClick()
        {
            Frm_Out.Instance.MdiParent = this;
            Frm_Out.Instance.Show();
            Frm_Out.Instance.Focus();
        }

        private void tsb_Plan_MyClick()
        {
            Frm_Input.Instance.MdiParent = this;
            Frm_Input.Instance.Show();
            Frm_Input.Instance.Focus();
            Frm_Input.Instance.UserId = User.UserName;
            Frm_Input.Instance.TextShowNPNum.Focus();
        }

        private void tsb_Prod_MyClick()
        {
            Frm_Prod.Instance.MdiParent = this;
            Frm_Prod.Instance.Show();
            Frm_Prod.Instance.Focus();
        }

        private void tsb_Query_MyClick()
        {
            Frm_Query.Instance.MdiParent = this;
            Frm_Query.Instance.Show();
            Frm_Query.Instance.Focus();
        }
    }
}

