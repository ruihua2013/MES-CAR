using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace test
{
    public partial class DemoForm : Form
    {

        public DemoForm()
        {
            InitializeComponent();
        }
        private ICSharpCode.SharpDevelop.Gui.IClipboardHandler iclipboard;
        private ICSharpCode.SharpDevelop.Gui.IEditBaseForm editbaseform;
        //Control ct1;
        IWindows windows;
        public DemoForm(System.Windows.Forms.Control ct)
        {
            InitializeComponent();
            //ct1 = ct;
            ct.Dock = DockStyle.Fill;
            editbaseform = ct as ICSharpCode.SharpDevelop.Gui.IEditBaseForm;
            iclipboard = ct as ICSharpCode.SharpDevelop.Gui.IClipboardHandler;
            windows = ct as IWindows;
            this.Size = ct.Size;
            this.panel1.Controls.Add(ct);
            this.FormClosing += new FormClosingEventHandler(DemoForm_FormClosing);
        }

        void DemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windows != null)
            {
                windows.Close();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null != editbaseform)
            {
                if(editbaseform.EnableFind)
                editbaseform.Find(this, null);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null != editbaseform)
            {
                if (editbaseform.EnableSave)
                    editbaseform.Save(this, null);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null != iclipboard)
            {
                if (iclipboard.EnableDelete)
                {
                    iclipboard.Delete();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null != editbaseform)
            {
                if (editbaseform.EnableNew)
                    editbaseform.New(this, null);
            }
        }
    }
}
