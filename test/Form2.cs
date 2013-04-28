using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WinControls.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Layout;
using AttnObjects;

namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.GridControl dataGrid = new DevExpress.XtraGrid.GridControl();
            DevExpress.XtraGrid.Views.Layout.LayoutView LayoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            dataGrid.MainView = LayoutView1;

            //DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptextedit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            //DevExpress.XtraGrid.Views.Layout.LayoutViewField Fd_col1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();

            // 
            // layoutViewField_colAddress
            // 
            //Fd_col1.EditorPreferredWidth = 127;
            //Fd_col1.Location = new System.Drawing.Point(0, 0);
            //Fd_col1.Name = "layoutViewField_colAddress";
            //Fd_col1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            //Fd_col1.Size = new System.Drawing.Size(197, 22);
            //Fd_col1.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            //Fd_col1.TextSize = new System.Drawing.Size(43, 13);

            DevExpress.XtraGrid.Columns.LayoutViewColumn col1 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            // 
            // colAddress
            // 
            col1.Caption = "编号";
            //col1.ColumnEdit = reptextedit;
            col1.CustomizationCaption = "编号";
            col1.FieldName = "ID_A";
            //col1.LayoutViewField = Fd_col1;
            col1.Name = "colno";
            col1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            col1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            col1.OptionsFilter.AllowFilter = false;


            LayoutView1.Columns.Add(col1);
            // dataGrid.RepositoryItems.Add(reptextedit);
            //ACodeGrid dataGrid= new ACodeGrid ();
            // EditGridLayout dataGrid = new EditGridLayout();
            dataGrid.Dock = DockStyle.Fill;
            //dataGrid.Prdknd = "04";

            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(dataGrid);

            //dataGrid.SetCaption("ZZ_ACODE");
            DataTable dt = CWData.ServerFactory.GetServer().GetDataTable("select * from ZZ_ACODE");
            dataGrid.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // dataGrid.RepositoryItems.Add(reptextedit);
            //ACodeGrid dataGrid= new ACodeGrid ();
            EditGridLayout dataGrid = new EditGridLayout();
           // LayoutView gridView1 = new LayoutView();
            dataGrid.Dock = DockStyle.Fill;
            //dataGrid.Prdknd = "04";
            //dataGrid.MainView = gridView1;
            this.panel1.Controls.Add(dataGrid);

           dataGrid.SetCaption("ZZ_ACODE");
            //LayoutViewColumn clm;
            //List<ZZ_UserViewDef> dicfds = ZZ_UserViewDef.GetTabView("ZZ_ACODE");
            //                foreach (ZZ_UserViewDef dct in dicfds)
            //                {
            //                    //clm = new CWGridColumn(dct);
            //                    clm = new LayoutViewColumn();
            //                    clm.Caption = dct.Caption;
            //                    clm.FieldName = dct.FldName;
            //                    // clm.OptionsColumn.AllowSort = _isSort?DefaultBoolean.True:DefaultBoolean.False;                    
            //                    // clm.VisibleIndex =clm.Visible?this.gridView1.Columns.Count:-1;
            //                    clm.CustomizationCaption = dct.Caption;
            //                    clm.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            //                    clm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //                    clm.OptionsFilter.AllowFilter = false;
            //                    clm.Name = dct.FldName;

            //                    gridView1.Columns.Add(clm);
            //                }
            DataTable dt = CWData.ServerFactory.GetServer().GetDataTable("select * from ZZ_ACODE");
            dataGrid.DataSource = dt;
        }
    }
}
