using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using AttnBarCode.PrintCenter;
using AttnObjects;

using DevExpress.XtraEditors.Repository;

using CWData;
using WinControls.Controls;

namespace MES_Car
{
    /// <summary>
    /// 称重基类
    /// </summary>
    public partial class UC_TaskList : UCEditBase,IWindows
    {
        protected EditGrid dataGrid;// = new EditGrid();

        protected DataTable curdt = null;
        protected string ViewName = "TaskList";
        private bool isdirty = false;
        IDBServer myServer = ServerFactory.GetServer();
        public UC_TaskList()
        {
            printBh = "0001";//打印模板编号
            InitializeComponent();
            this.dateTimePicker1.Value = this.dateTimePicker2.Value = System.DateTime.Now;

        }
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {

                myServer = ServerFactory.GetServer();

                ICSharpCode.Core.Properties pro = ICSharpCode.Core.PropertyService.Get("DataGridOption", new ICSharpCode.Core.Properties());
                dataGrid = new EditGrid();

                dataGrid.Dock = DockStyle.Fill;

                dataGrid.OnValidatingEditor += new BaseContainerValidateEditorEventHandler(dataGrid_OnValidatingEditor);
                dataGrid.OnCreateColumning += new OnCreateColumnsHadle(dataGrid_OnCreateColumning);
                dataGrid.SetCaption(ViewName);
                dataGrid.OnCreateContextMenu += new OnCreateContextMenuHadle(dataGrid_OnCreateContextMenu);
                this.panel2.Controls.Add(dataGrid);
                
                //dataGrid.ShowEditor();

                LoadData();
            }
            
        }

        void dataGrid_OnValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            isdirty = true;
        }

        void dataGrid_Validating(object sender, CancelEventArgs e)
        {
            isdirty=true;
        }

        void dataGrid_OnCreateContextMenu(ContextMenu menu, PopupMenuShowingEventArgs e)
        {
            if (!e.HitInfo.InColumn && (this.dataGrid.SelectedRowsCount > 0))
            {
                MenuItem menu_packing = new MenuItem("快速设置为相同值");
                menu_packing.Click += new EventHandler(menu_packing_Click);
                menu.MenuItems.Add(menu_packing);
            }
        }

        void dataGrid_OnCreateColumning(ZZ_TAB_VIEW dct, out RepositoryItem RepositoryItem)
        {
            RepositoryItem = null;
            if (dct.FldName == "DNO")
            {
                //RepositoryItemComboBox combos = new RepositoryItemComboBox();

                string sql = "select DNO,Name from ZZ_Device ";
                DataTable dt = ServerFactory.GetServer().GetDataTable(sql);

                DevExpress.XtraGrid.Columns.GridColumn col_no, col_name;
                col_no = new DevExpress.XtraGrid.Columns.GridColumn();

                col_no.Caption = "编号";
                col_no.FieldName = "DNO";
                col_no.Name = "col_no";
                col_no.Visible = true;
                col_no.VisibleIndex = 1;
                col_no.Width = 90;

                col_name = new DevExpress.XtraGrid.Columns.GridColumn();
                col_name.Caption = "名称";
                col_name.FieldName = "Name";
                col_name.Name = "col_name";
                col_name.Visible = true;
                col_name.VisibleIndex = 1;
                col_name.Width = 120;


                GridView repositoryItemGridLookUpEdit1View = new GridView();
                repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                col_no,
                col_name});
                repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
                repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
                repositoryItemGridLookUpEdit1View.OptionsView.ShowFooter = true;
                repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
                repositoryItemGridLookUpEdit1View.OptionsView.ShowFooter = false;

                DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
                repositoryItemGridLookUpEdit1.AutoHeight = false;
                repositoryItemGridLookUpEdit1.Buttons.Clear();
                repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
                repositoryItemGridLookUpEdit1.DataSource = dt;
                repositoryItemGridLookUpEdit1.DisplayMember = "Name";
                repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
                repositoryItemGridLookUpEdit1.PopupFormSize = new System.Drawing.Size(550, 0);
                repositoryItemGridLookUpEdit1.ValueMember = "DNO";
                repositoryItemGridLookUpEdit1.View = repositoryItemGridLookUpEdit1View;

                //    //        ((System.ComponentModel.ISupportInitialize)(xpServerCollectionSource1)).EndInit();

                RepositoryItem = repositoryItemGridLookUpEdit1;

            }
        }


        void ckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ckbox = sender as CheckEdit;
            if (null != ckbox)
            {
                DataRow dr = dataGrid.GetFocusedDataRow();
                if (dr["ID_B"].ToString().Length > 0)//如果已经组成托了，就不能选中了
                {
                    ckbox.Checked = false;
                }
            }
        }

        void dataGrid_OnValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (dataGrid.FocusedRowHandle >= 0)
            {
                DataRow dataRow = dataGrid.GetDataRow(dataGrid.FocusedRowHandle);
                if (null != dataRow)
                {
                    if (dataRow["ID_B"].ToString().Length > 0)
                    {
                        e.ErrorText = "已经组成托(箱)，不能修改！";
                        e.Valid = false;
                    }
                }
            }
            else
            {
                string mes = "";
                e.Valid = dataGrid.CheckBaseRow(dataGrid.FocusedRowHandle,out mes);
                if (!e.Valid)
                {
                    e.ErrorText = mes;
                }
            }
        }

        //快速设置为相同值
        void menu_packing_Click(object sender, EventArgs e)
        {
            isdirty = true;
            int[]rows=dataGrid.GetSelectRows();
            if(rows.Length>0)
            {
                string dno= dataGrid.GetDataRow(rows[0])["DNO"].ToString();
                if (dno.Length > 0)
                {
                    for (int r = 1; r < rows.Length; r++)
                    {
                        dataGrid.SetRowValue(rows[r], "DNO", dno);
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
            isdirty = false;
        }
        protected virtual void LoadData()
        {
            StringBuilder sbsql = new StringBuilder(103);
            sbsql.Append("select * from V_Task where PackTime is null ");
            if (this.txt_plano.Text.Length>0)
            {
                sbsql.Append(string.Format(" and PlanID like '%{0}%'",this.txt_plano.Text));
                //sbsql.Append(string.Format(" and (convert(varchar(10),WT_DD,20)>='{0}' and convert(varchar(10),WT_DD,20)<='{1}')", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), this.dateTimePicker2.Value.ToString("yyyy-MM-dd")));
            }
            if (this.txt_taskno.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and TaskID like '%{0}%'", this.txt_taskno.Text));
                //sbsql.Append(string.Format(" and (convert(varchar(10),WT_DD,20)>='{0}' and convert(varchar(10),WT_DD,20)<='{1}')", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), this.dateTimePicker2.Value.ToString("yyyy-MM-dd")));
            }
            //if (this.txt_prdname.Text.Length > 0)
            //{
            //    sbsql.Append(string.Format(" and PRD_NAME like '%{0}%'", this.txt_prdname.Text));
            //}
            //if (this.txt_prdspc.Text.Length > 0)
            //{
            //    sbsql.Append(string.Format(" and PRD_SPC like '%{0}%'", this.txt_prdspc.Text));
            //}
            //if (this.radioButton1.Checked)
            //{
            //    sbsql.Append(" and (ID_B='' or ID_B is null)");
            //}
            //else if (this.radioButton2.Checked)
            //{
            //    sbsql.Append(" and len(ID_B )>0");
            //}
            sbsql.Append(" order by TaskId Desc");
            string sql = sbsql.ToString();
            dataGrid.DataSource = curdt = myServer.GetDataTable(sql);
        }

        public override bool EnableDelete
        {
            get
            {
                return dataGrid.FocusedRowHandle>=0;
            }
        }
        public override bool EnableFind
        {
            get { return false; }
        }

        public override bool EnableNew
        {
            get { return false; }
        }

        public override bool EnableSave
        {
            get { return this.isdirty; }
        }
        public override void Delete()
        {
            //int i = dataGrid.FocusedRowHandle;
            //DevExpress.XtraGrid.Views.Base.RowObjectEventArgs eas = new DevExpress.XtraGrid.Views.Base.RowObjectEventArgs(i, dataGrid.GetFocusedDataRow());
            //dataGrid.dataGrid_OnRowDeleted(this, eas);
        }
        public override bool Save(object sender, EventArgs e)
        {
            dataGrid.MainView.PostEditor();
            dataGrid.MainView.CloseEditor();
            dataGrid.Focus();
            if (curdt == null) return false;
            string sql = "update t_task set DNO='{0}' where TaskID='{1}'";
            foreach (DataRow dr in curdt.Rows)
            {
                if (dr.RowState == DataRowState.Modified)
                {
                    string sql1 = string.Format(sql, dr["DNO"], dr["TaskID"]);
                    myServer.ExecuteSQL(sql1, null);
                }
            }
            MessageService.ShowMessage("保存成功！");
            return true;
        }

        #region IPrint 成员
        protected override object GetPrintData()
        {
            DataTable pdt = curdt.Clone();
            DataRow currow = dataGrid.GetFocusedDataRow();
            if (null != currow)
            {
                pdt.Rows.Add(currow.ItemArray);
                pdt.AcceptChanges();
            }
            return pdt;
        }
        public override void Print()
        {
            if (null == printemp)
            {
                printemp = PrintTemplete.GetDefaultTemplete(printBh);
            }
            DataTable pdt = curdt.Clone();
            try
            {
                XtraReport report = printemp.GetReport();
                DataRow currow = dataGrid.GetFocusedDataRow();
                if (null == currow)
                {
                    MessageService.ShowWarning("请选择要打印的记录！");
                    return;
                }
                pdt.Rows.Add(currow.ItemArray);
                pdt.AcceptChanges();
                report.DataSource = pdt;
                PrintReport(report);
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }

        }
        #endregion

        #region IWindows 成员

        public void Close()
        {
            //if (null != this.ewatch1)
            //{
            //    ewatch1.Close();
            //}
        }

        #endregion


    }
}
