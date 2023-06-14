using Octokit;

namespace SteamDlcShopping.Core.Controllers;

public static class CoreController
{
    public static async Task<string?> GetLatestVersionName()
    {
        //Get latest release from GitHub
        //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/

        GitHubClient client = new(new ProductHeaderValue("SteamDlcShopping"));
        Release release = await client.Repository.Release.GetLatest("DiogoABDias", "SteamDlcShopping");

        Version latestGitHubVersion = new(release.TagName.Replace("v", ""));
        Version localVersion = new("1.1.0");
        int versionComparison = localVersion.CompareTo(latestGitHubVersion);

        return versionComparison < 0 ? release.Name : null;
    }

    public static void OpenLink(string url)
    {
        Process process = new()
        {
            StartInfo = new()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = "cmd.exe",
                Arguments = $"/c start {url}"
            }
        };

        process.Start();
    }
}