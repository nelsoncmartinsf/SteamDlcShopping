# Steam DLC Shopping

This desktop application lets you browse all the DLC from games you own on Steam.

Currently there's no place on Steam's website to view this information all at once, making it difficult to know what games are missing DLC to purchase for accounts with large libraries.

With this application you can browse and manage through your games list, see what DLC you're missing for each game and if they are on sale.

<br/>

## Development

This application is currently under active development, with bugs being fixed and new features being added over time.

You can contribute to this project by reporting bugs you find or suggesting new features that can improve the general user experience.

<br/>

## Frequently Asked Question (FAQ)

**Q.** &emsp; How do I use this application?

**A.** &emsp; Downloading the latest release is the recommended behaviour. Then you can launch it, log in with your Steam account and press to calculate your library.

<br/><br/>

**Q.** &emsp; Why is this a desktop application I have to download and log in instead of a simple website as many other Steam tools out there?

**A.** &emsp; Initially this project was going to be a web page where you could log in with Steam OpenID integration, however the required cookie management for this to work properly is blocked from a website control for security reasons (CORS), so that idea had to be dropped.

<br/><br/>

**Q.** &emsp; If it's not possible for a website because of security reasons, then should I be worried using this application?

**A.** &emsp; No account details information is ever accessed! This application only requires you to login to Steam in order to use your session cookies to retrieve your games.
This is needed because Steam API doesn't provide user libraries, however oddly enough they have a link with that information available. The url used to retrieve the games list is [this one](https://store.steampowered.com/dynamicstore/userdata/), you can access it yourself (make sure you are logged in to Steam on your browser).

<br/>

## License

This is a free open source software licensed under the GNU GPLv3, which lets you freely use, modify and redistribute under the terms of the GNU General Public License  by the Free Software Foundation.
A copy of this license is included in the repository.
