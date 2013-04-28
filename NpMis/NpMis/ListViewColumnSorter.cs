namespace NpMis
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class ListViewColumnSorter : IComparer
    {
        private int ColumnToSort = 0;
        private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
        private SortOrder OrderOfSort = SortOrder.Descending;

        public int Compare(object x, object y)
        {
            ListViewItem item = (ListViewItem) x;
            ListViewItem item2 = (ListViewItem) y;
            int num = this.ObjectCompare.Compare(item.SubItems[this.ColumnToSort].Text, item2.SubItems[this.ColumnToSort].Text);
            if (this.OrderOfSort == SortOrder.Ascending)
            {
                return num;
            }
            if (this.OrderOfSort == SortOrder.Descending)
            {
                return -num;
            }
            return 0;
        }

        public SortOrder Order
        {
            get
            {
                return this.OrderOfSort;
            }
            set
            {
                this.OrderOfSort = value;
            }
        }

        public int SortColumn
        {
            get
            {
                return this.ColumnToSort;
            }
            set
            {
                this.ColumnToSort = value;
            }
        }
    }
}

