using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SteamDlcShopping.Core.Controllers;
using ZipArchive = SharpCompress.Archives.Zip.ZipArchive;

namespace SteamDlcShopping.Updater;

public partial class FrmMain : Form
{
    public FrmMain() => InitializeComponent();

    private async void FrmMain_Shown(object sender, EventArgs e)
    {
        try
        {
            lblProgress.Text = "Downloading the latest release...";

            string url = await CoreController.GetLatestVersionUrl();
            using Stream stream = await new HttpClient().GetStreamAsync(url);
            using FileStream fileStream = new("SteamDlcShopping.zip", FileMode.CreateNew);
            await stream.CopyToAsync(fileStream);
            fileStream.Close();

            lblProgress.Text = "Unzipping the contents...";

            using ZipArchive archive = ZipArchive.Open("SteamDlcShopping.zip");
            foreach (ZipArchiveEntry entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                entry.WriteToDirectory(".", new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }

            lblProgress.ForeColor = Color.Green;
            lblProgress.Text = "SteamDlcShopping successfully updated!";
        }
        catch (Exception ex)
        {
            lblProgress.ForeColor = Color.Red;
            lblProgress.Text = "An error was encountered updating SteamDlcShopping!";
            CoreController.LogException(ex);
        }

        await Task.Delay(5000);
        Close();
    }
}