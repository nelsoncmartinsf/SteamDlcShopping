namespace SteamDlcShopping.Extensibility
{
    internal class ListViewPlus : ListView
    {
        internal protected void Unload()
        {
            Items.Clear();
            ListViewItemSorter = null;
            ResetHeaders();
        }

        internal protected void ResetHeaders()
        {
            foreach (ColumnHeader column in Columns)
            {
                if (column.Tag is null)
                {
                    continue;
                }

                if (!int.TryParse(column.Tag.ToString(), out int length))
                {
                    continue;
                }

                column.Text = column.Text[..length];
            }
        }
    }
}