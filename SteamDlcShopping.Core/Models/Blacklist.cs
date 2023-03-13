using Newtonsoft.Json;

namespace SteamDlcShopping.Core.Models
{
    internal class Blacklist
    {
        //Properties
        internal List<GameBlacklist>? Games { get; private set; }

        //Methods
        internal void Load(long steamId)
        {
            if (!File.Exists($"{steamId}.txt"))
            {
                return;
            }

            string content = File.ReadAllText($"{steamId}.txt");
            Games = JsonConvert.DeserializeObject<List<GameBlacklist>>(content);
        }

        internal void Save(long steamId)
        {
            if (Games is null)
            {
                return;
            }

            string content = JsonConvert.SerializeObject(Games);
            File.WriteAllText($"{steamId}.txt", content);
        }

        internal void AddGame(int appId, string name, bool autoBlacklisted = true)
        {
            Games ??= new();

            if (Games.Any(x => x.AppId == appId))
            {
                return;
            }

            GameBlacklist gameBlacklist = new(appId, name, autoBlacklisted);
            Games.Add(gameBlacklist);
        }

        internal void RemoveGame(int appId)
        {
            if (Games is null)
            {
                return;
            }

            if (!Games.Any(x => x.AppId == appId))
            {
                return;
            }

            int index = Games.FindIndex(x => x.AppId == appId);
            Games.RemoveAt(index);
        }
    }
}