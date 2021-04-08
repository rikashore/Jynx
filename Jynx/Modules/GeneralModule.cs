using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Attributes;
using Jynx.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Modules
{
    public class GeneralModule : BaseCommandModule
    {
        public Jynx jynx { private get; set; }
        public Configuration configuration { private get; set; }

        [Command("ping")]
        [Description("pong! Check if the bot is alive using this command")]
        [Aliases("pong")]
        [Usage("jxping")]
        public async Task Ping(CommandContext ctx)
        {
            var pingEmbed = new DiscordEmbedBuilder()
                .WithTitle("Pomg!")
                .WithDescription($"Jynx's latency is `{jynx.latency}ms`")
                .WithColor(JynxCosmetics.JynxColor)
                .Build();

            await ctx.RespondAsync(pingEmbed);
        }

        [Command("info")]
        [Description("Get some info about Jynx")]
        [Usage("jxinfo")]
        public async Task Info(CommandContext ctx)
        {
            var infoEmbed = new DiscordEmbedBuilder()
                .WithTitle("Jynx info")
                .WithThumbnail(jynx.avatar)
                .WithFooter("Made by shift-eleven#7304")
                .WithColor(JynxCosmetics.JynxColor)
                .AddField("Version", configuration.Version)
                .AddField("Prefixes", string.Join(",", configuration.Prefixes))
                .AddField("Github", "[Jynx](https://github.com/shift-eleven/Jynx)")
                .Build();

            await ctx.RespondAsync(infoEmbed);
        }
    }
}
