using SteamDlcShopping.Dtos;

namespace SteamDlcShopping
{
    public partial class FrmBlacklist : Form
    {
        public FrmBlacklist()
        {
            InitializeComponent();

            _filterName = string.Empty;
            _blacklist = new();
        }

        //////////////////////////////////////// FORM ////////////////////////////////////////

        private void FrmBlacklist_Load(object sender, EventArgs e)
        {
            _blacklist = Middleware.GetBlacklist();

            lblGameCount.Text = null;
            btnRemove.Enabled = false;

            lsbBlacklist.DisplayMember = "Name";

            LoadBlacklistToListbox();
        }

        //////////////////////////////////////// LISTBOX ////////////////////////////////////////

        public List<GameBlacklistDto> _blacklist;

        private void LoadBlacklistToListbox()
        {
            lsbBlacklist.Items.Clear();

            lsbBlacklist.BeginUpdate();

            foreach (GameBlacklistDto game in _blacklist)
            {
                //Filter by name search
                if (string.IsNullOrWhiteSpace(game.Name) || !game.Name.Contains(_filterName, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                lsbBlacklist.Items.Add(game);
            }

            lsbBlacklist.EndUpdate();

            //Fill in metric fields
            lblGameCount.Text = $"Count: {lsbBlacklist.Items.Count}";

            btnClearAutoBlacklisted.Enabled = _blacklist.Any(x => x.AutoBlacklisted);
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

        //////////////////////////////////////// FILTERS ////////////////////////////////////////

        private string _filterName;

        private void txtBlacklistSearch_TextChanged(object sender, EventArgs e)
        {
            _filterName = txtBlacklistSearch.Text;
            btnRemove.Enabled = false;

            LoadBlacklistToListbox();
        }

        //////////////////////////////////////// BUTTONS ////////////////////////////////////////

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<int> _unblacklist = new();

            for (int index = lsbBlacklist.SelectedItems.Count - 1; index >= 0; index--)
            {
                GameBlacklistDto game = (GameBlacklistDto)lsbBlacklist.SelectedItems[index];

                _unblacklist.Add(game.AppId);
                _blacklist.Remove(game);
            }

            if (_unblacklist.Any())
            {
                Middleware.UnblacklistGames(_unblacklist);
                LoadBlacklistToListbox();
            }
        }

        private void btnClearAutoBlacklisted_Click(object sender, EventArgs e)
        {
            List<int> _unblacklist = new();

            for (int index = _blacklist.Count - 1; index >= 0; index--)
            {
                GameBlacklistDto game = _blacklist[index];

                if (!game.AutoBlacklisted)
                {
                    continue;
                }

                _unblacklist.Add(game.AppId);
                _blacklist.Remove(game);
            }

            if (_unblacklist.Any())
            {
                Middleware.UnblacklistGames(_unblacklist);
                LoadBlacklistToListbox();
            }
        }
    }
}