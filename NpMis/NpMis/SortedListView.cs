namespace NpMis
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class SortedListView : ListView
    {
        private IContainer components;
        private ListViewColumnSorter lvwColumnSorter;

        public SortedListView()
        {
            this.InitializeComponent();
            this.lvwColumnSorter = new ListViewColumnSorter();
            base.ListViewItemSorter = this.lvwColumnSorter;
            base.ColumnClick += new ColumnClickEventHandler(this.SortedListView_ColumnClick);
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

        private void SortedListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == this.lvwColumnSorter.SortColumn)
            {
                if (this.lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    this.lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    this.lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                this.lvwColumnSorter.SortColumn = e.Column;
                this.lvwColumnSorter.Order = SortOrder.Ascending;
            }
            base.Sort();
        }

        public SortOrder Order
        {
            get
            {
                return this.lvwColumnSorter.Order;
            }
            set
            {
                this.lvwColumnSorter.Order = value;
            }
        }

        public int SortColumn
        {
            get
            {
                return this.lvwColumnSorter.SortColumn;
            }
            set
            {
                this.lvwColumnSorter.SortColumn = value;
            }
        }
    }
}

