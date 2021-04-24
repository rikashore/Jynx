using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Common;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using Jynx.Common.Attributes;

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
                .WithTitle("Pong!")
                .WithDescription($"Jynx's latency is `{ctx.Client.Ping}ms`")
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
                .AddField("Version", configuration.Version, true)
                .AddField("Prefixes", string.Join(",", configuration.Prefixes.Select(Formatter.InlineCode)), true)
                .AddField("Github", "[Jynx](https://github.com/shift-eleven/Jynx)")
                .Build();

            await ctx.RespondAsync(infoEmbed);
        }

        [Command("serverinfo")]
        [Description("Get some info about your server")]
        [Usage("jxserverinfo")]
        public async Task ServerInfo(CommandContext ctx)
        {
            var serverEmbed = new DiscordEmbedBuilder()
                .WithTitle(ctx.Guild.Name)
                .WithColor(JynxCosmetics.JynxColor)
                .WithThumbnail(ctx.Guild.IconUrl)
                .AddField("Server owner", ctx.Guild.Owner.Username)
                .AddField("Users", $"{ctx.Guild.Members.Values.Count(x => x.IsBot != true)}")
                .AddField("Bots", $"{ctx.Guild.Members.Values.Count(x => x.IsBot)}")
                .AddField("Created at", $"{ctx.Guild.CreationTimestamp}")
                .Build();

            await ctx.RespondAsync(serverEmbed);
        }
    }
}
