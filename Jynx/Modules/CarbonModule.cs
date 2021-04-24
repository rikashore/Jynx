using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Common;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Jynx.Common.Attributes;
using Jynx.Services;
using Extensions = Emzi0767.Extensions;

namespace Jynx.Modules
{
    public class CarbonModule : BaseCommandModule
    {
        public CarbonService CarbonService { private get; set; }
        
        [Command("snippet")]
        [Description("Allows you to turn a code block into a code snippet")]
        [Aliases("cs")]
        [Usage("jxsnippet [theme] [code block for snippet]")]
        public async Task CarbonCode(CommandContext ctx, string theme, [RemainingText] string code)
        {
            if (theme != null && code == null)
            {
                await ctx.Channel.SendMessageAsync("Using this command requires you to specify a theme, use `jx themes` to get a list of all available themes");
                return;
            }

            if (!JynxExtensions.TryParseCodeBlock(code, out code))
            {
                await ctx.RespondAsync("You need to wrap the code in a code block");
                return;
            }

            var cs = code.ParseCodeBlock();

            cs = cs.Remove(cs.LastIndexOf("\n", StringComparison.OrdinalIgnoreCase));
            var csUrl = WebUtility.UrlEncode(cs);

            string validTheme = CarbonService.ThemeMatcher(theme);

            var codeEmbed = CarbonService.BuildCarbonEmbed(ctx.Member.Username, validTheme, csUrl);

            await ctx.Channel.SendMessageAsync(codeEmbed);
        }

        [Command("themes")]
        [Description("Get a list of all the available themes")]
        [Aliases("snippet-themes")]
        [Usage("jxthemes")]
        public async Task ListThemes(CommandContext ctx)
        {
            string[] lightThemes = CarbonService.GetLightThemes();
            string[] darkThemes = CarbonService.GetDarkThemes();

            var themesEmbed = new DiscordEmbedBuilder()
                .WithTitle("Available themes")
                .WithDescription("You can use these themes with the code command")
                .AddField("Dark themes", darkThemes.NewlineQuote())
                .AddField("Light Themes", lightThemes.NewlineQuote())
                .WithColor(JynxCosmetics.JynxColor)
                .Build();

            await ctx.Channel.SendMessageAsync(themesEmbed);
        }

        [Command("theme")]
        [Description("individual theme images")]
        [Usage("jxtheme [theme name]")]
        public async Task Theme(CommandContext ctx, string theme)
        {
            string[] themes = CarbonService.GetThemes();

            if (!Array.Exists(themes, x => x == theme))
            {
                await ctx.Channel.SendMessageAsync("The theme provided does not match any themes use `jx themes` to get a list of available themes");
                return;
            }

            await ctx.TriggerTypingAsync();

            string path = $"./Images/Snippets/{theme}-theme.png";

            await new DiscordMessageBuilder()
                .WithContent($"Theme snippet for {theme}")
                .WithFile(path, File.OpenRead(path))
                .SendAsync(ctx.Channel);
        }
    }
}
