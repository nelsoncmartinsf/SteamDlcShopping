using System.Collections;

namespace SteamDlcShopping
{
    public class ListViewColumnSorter : IComparer
    {
        //Fields
        private int _column;

        private SortOrder _order;

        private readonly CaseInsensitiveComparer _comparer;

        //Properties
        public int Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        public SortOrder Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }

        //Constructor
        public ListViewColumnSorter()
        {
            _column = 0;
            _order = SortOrder.None;
            _comparer = new CaseInsensitiveComparer();
        }

        //Methods
        public int Compare(object x, object y)
        {
            ListViewItem listviewX = (ListViewItem)x;
            ListViewItem listviewY = (ListViewItem)y;
            int compareResult;

            compareResult = _comparer.Compare(listviewX.SubItems[_column].Text, listviewY.SubItems[_column].Text);

            return _order switch
            {
                SortOrder.Ascending => compareResult,
                SortOrder.Descending => (-compareResult),
                _ => 0
            };
        }
    }
}