using System.Collections.Generic;
using System.Linq;
using DSharpPlus.Entities;
using Jynx.Common;
using Jynx.Common.Attributes;

namespace Jynx.Services
{
    [JynxService]
    public class CarbonService
    {
        
        private static readonly Dictionary<string, string> ThemeDict = new Dictionary<string, string>
        {
            {"draculapro", "dark" },
            {"vscode", "dark" },
            {"3024night", "dark" },
            {"a11ydark", "dark" },
            {"blackboard", "dark" },
            {"base16(dark)", "dark" },
            {"base16(light)", "light" },
            {"cobalt", "dark" },
            {"duotone", "dark" },
            {"hopscotch", "dark" },
            {"lucario", "dark" },
            {"material", "dark" },
            {"nonokai", "dark" },
            {"nightowl", "dark" },
            {"nord", "dark" },
            {"oceanicnext", "dark" },
            {"onelight", "light" },
            {"onedark", "dark" },
            {"panda", "dark" },
            {"paraiso", "dark" },
            {"seti", "dark" },
            {"shadesofpurple", "dark" },
            {"solarizeddark", "dark" },
            {"solarizedlight", "light" },
            {"synthwave84", "dark" },
            {"twilight", "dark" },
            {"verminal", "dark" },
            {"yeti", "light" },
            {"zenburn", "dark" }
        };

        public string[] GetDarkThemes()
        {
            var darkThemes = ThemeDict.Where(x => x.Value == "dark").Select(x => x.Key).ToArray();
            return darkThemes;
        }

        public string[] GetLightThemes()
        {
            var lightThemes = ThemeDict.Where(x => x.Value == "light").Select(x => x.Key).ToArray();
            return lightThemes;
        }

        public string[] GetThemes()
        {
            var themes = ThemeDict.Keys;
            return themes.ToArray();
        }

        public DiscordEmbed BuildCarbonEmbed(string username, string theme, string code)
        {
            var codeEmbed = new DiscordEmbedBuilder()
                .WithTitle($"Code snippet by {username}")
                .WithDescription("You can modify the snippet to your liking by following the link!")
                .WithUrl($"https://carbon.now.sh/?l=auto&t={theme}&code={code}")
                .WithFooter("Snippets made by carbon")
                .WithColor(JynxCosmetics.JynxColor)
                .Build();

            return codeEmbed;
        }

        public string ThemeMatcher(string theme)
        {
            var themeInput = theme.ToLower();
            var validTheme = themeInput switch
            {
                "draculapro" => "dracula-pro",
                "vscode" => "vscode",
                "3024night" => "3024-night",
                "a11ydark" => "a11y-dark",
                "blackboard" => "blackboard",
                "base16(dark)" => "base16-dark",
                "base16(light)" => "base16-light",
                "cobalt" => "cobalt",
                "duotone" => "duotone-dark",
                "hopscotch" => "hopscotch",
                "lucario" => "lucario",
                "material" => "material",
                "monokai" => "monokai",
                "nightowl" => "night-owl",
                "nord" => "nord",
                "oceanicnext" => "oceanic-next",
                "onelight" => "one-light",
                "onedark" => "one-dark",
                "panda" => "panda",
                "paraiso" => "paraiso-dark",
                "seti" => "seti",
                "shadesofpurple" => "shades-of-purple",
                "solarizeddark" => "solarized+dark",
                "solarizedlight" => "solarized+light",
                "synthwave84" => "synthwave-84",
                "twilight" => "twilight",
                "verminal" => "verminal",
                "yeti" => "yeti",
                "zenburn" => "zenburn",
                _ => "vscode"
            };

            return validTheme;
        }
    }
}