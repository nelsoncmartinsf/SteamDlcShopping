using SteamDlcShopping.Dtos;

namespace SteamDlcShopping
{
    public partial class FrmBlacklist : Form
    {
        public FrmBlacklist()
        {
            InitializeComponent();

            _filterName = string.Empty;
        }

        //////////////////////////////////////// FORM ////////////////////////////////////////

        private void FrmBlacklist_Load(object sender, EventArgs e)
        {
            lsbBlacklist.DisplayMember = "Name";

            LoadBlacklistToListbox();
        }

        //////////////////////////////////////// LISTBOX ////////////////////////////////////////

        public List<GameBlacklistDto>? _blacklist;

        private void LoadBlacklistToListbox()
        {
            _blacklist = Middleware.GetBlacklist(_filterName, _filterAutoBlacklisted);

            lsbBlacklist.Items.Clear();

            lsbBlacklist.BeginUpdate();

            foreach (GameBlacklistDto game in _blacklist)
            {
                lsbBlacklist.Items.Add(game);
            }

            lsbBlacklist.EndUpdate();

            //Fill in metric fields
            lblGameCount.Text = lsbBlacklist.Items.Count > 0 ? $"Count: {lsbBlacklist.Items.Count}" : null;

            btnRemove.Enabled = false;
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
        private bool _filterAutoBlacklisted;

        private void txtBlacklistSearch_TextChanged(object sender, EventArgs e)
        {
            _filterName = txtBlacklistSearch.Text;

            LoadBlacklistToListbox();
        }

        private void chkHideAutoBlacklistedGames_CheckedChanged(object sender, EventArgs e)
        {
            _filterAutoBlacklisted = chkHideAutoBlacklistedGames.Checked;

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

            if (_blacklist is null)
            {
                return;
            }

            for (int index = _blacklist.Count - 1; index >= 0; index--)
            {
                GameBlacklistDto game = _blacklist[index];

                if (!game.AutoBlacklisted)
                {
                    continue;
                }

                _unblacklist.Add(game.AppId);
            }

            if (_unblacklist.Any())
            {
                Middleware.UnblacklistGames(_unblacklist);
                LoadBlacklistToListbox();
            }
        }
    }
}