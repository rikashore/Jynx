using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Common;
using Jynx.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Modules
{
    public class CarbonModule : BaseCommandModule
    {
        [Command("snippet")]
        [Description("Allows you to turn a code block into a code snippet")]
        [Aliases("cs")]
        public async Task CarbonCode(CommandContext ctx, string theme, [RemainingText] string code)
        {
            if (theme != null && code == null)
            {
                await ctx.Channel.SendMessageAsync("Using this command requires you to specify a theme, use `jx themes` to get a list of all available themes");
                return;
            }

            var cs = await code.ParseCodeBlock(ctx, ParseType.Default);

            if (cs == null)
                return;

            cs = cs.Remove(cs.LastIndexOf("\n"));
            var csUrl = WebUtility.UrlEncode(cs);

            string validTheme = CarbonHandler.ThemeMatcher(theme);

            await ctx.Channel.SendCarbonCodeAsync(ctx.Member.Username, validTheme, csUrl);
        }

        [Command("themes")]
        [Description("Get a list of all the available themes")]
        [Aliases("snippet-themes")]
        public async Task ListThemes(CommandContext ctx)
        {
            string[] lightThemes = CarbonHandler.GetLightThemes();
            string[] darkThemes = CarbonHandler.GetDarkThemes();

            var themesEmbed = new DiscordEmbedBuilder()
                .WithTitle("Available themes")
                .WithDescription("You can use these themes with the code command")
                .AddField("Dark themes", $"> {string.Join("\n> ", darkThemes)}")
                .AddField("Light Themes", $"> {string.Join("\n> ", lightThemes)}")
                .WithColor(JynxCosmetics.JynxColor)
                .Build();

            await ctx.Channel.SendMessageAsync(themesEmbed);
        }

        [Command("theme")]
        [Description("individual theme images")]
        public async Task Theme(CommandContext ctx, string theme)
        {
            string[] themes = CarbonHandler.GetThemes();

            if (!Array.Exists(themes, x => x == theme))
            {
                await ctx.Channel.SendMessageAsync("The theme provided does not match any themes use `jx themes` to get a list of available themes");
                return;
            }

            await ctx.TriggerTypingAsync();

            string path = $"./Images/Snippets/{theme}-theme.png";

            var msg = await new DiscordMessageBuilder()
                .WithContent($"Theme snippet for {theme}")
                .WithFile(path, File.OpenRead(path))
                .SendAsync(ctx.Channel);
        }
    }
}
