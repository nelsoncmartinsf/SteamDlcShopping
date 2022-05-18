namespace SteamDlcShopping
{
    public partial class FrmBlacklist : Form
    {
        private string _filterName = string.Empty;
        public SortedDictionary<int, string> _blacklist;
        private List<int> _unblacklist;

        public FrmBlacklist()
        {
            InitializeComponent();
        }

        private void FrmBlacklist_Load(object sender, EventArgs e)
        {
            _blacklist = Middleware.GetBlacklist();
            _unblacklist = new();
            btnRemove.Enabled = false;

            lsbBlacklist.DisplayMember = "Value";
            lsbBlacklist.ValueMember = "Key";

            LoadBlacklistToListbox();
        }

        private void lsbBlacklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsbBlacklist.SelectedIndices.Count == 0)
            {
                btnRemove.Enabled = false;
                return;
            }

            btnRemove.Enabled = true;
        }

        private void txtBlacklistSearch_TextChanged(object sender, EventArgs e)
        {
            _filterName = txtBlacklistSearch.Text;
            LoadBlacklistToListbox();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int idx = lsbBlacklist.SelectedItems.Count - 1; lsbBlacklist.SelectedItems.Count > 0; idx--)
            {
                KeyValuePair<int, string> item = (KeyValuePair<int, string>)lsbBlacklist.SelectedItems[idx];

                _unblacklist.Add(item.Key);
                lsbBlacklist.Items.Remove(item);
            }

            Middleware.UnblacklistGames(_unblacklist);
            _unblacklist = new();

            //Fill in metric fields
            lblGameCount.Text = $"Count: {lsbBlacklist.Items.Count}";
        }

        private void LoadBlacklistToListbox()
        {
            lsbBlacklist.Items.Clear();

            lsbBlacklist.BeginUpdate();

            foreach (KeyValuePair<int, string> item in _blacklist)
            {
                //Filter by name search
                if (!item.Value.Contains(_filterName, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                lsbBlacklist.Items.Add(item);
            }

            lsbBlacklist.EndUpdate();

            //Fill in metric fields
            lblGameCount.Text = $"Count: {lsbBlacklist.Items.Count}";
        }
    }
}