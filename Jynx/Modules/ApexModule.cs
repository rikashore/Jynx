using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Jynx.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jynx.Common.Attributes;
using Jynx.Services;

namespace Jynx.Modules
{
    public class ApexModule : BaseCommandModule
    {
        public Configuration _configuration { private get; set; }
        public HttpClient _httpclient { private get; set; }
        public ApexService ApexService { private get; set; }

        [Command("stats")]
        [Description("Grab the stats of an Apex Legends player")]
        [Aliases("stat")]
        [Usage("jxstats [PC/PS4/X1] [username]")]
        public async Task PlayerStats(CommandContext ctx, string platform, [RemainingText] string username)
        {
            await ctx.TriggerTypingAsync();

            string result;
            try
            {
                result = await _httpclient.GetStringAsync($"https://api.mozambiquehe.re/bridge?version=5&platform={platform}&player={username}&auth={_configuration.ApiTrackerKey}");
            }
            catch (Exception)
            {
                await ctx.Channel.SendMessageAsync("Couldn't fetch user data");
                return;
            }

            var stats = ApexService.GetPlayerStats(result);
            var embeds = ApexService.BuildStatEmbeds(stats);

            var statEmbed = embeds[0];
            var rankEmbed = embeds[1];

            await ctx.RespondAsync(statEmbed);
            await ctx.RespondAsync(rankEmbed);
        }

        [Command("rotation")]
        [Description("Check the current map rotation for Apex Legends")]
        [Aliases("map-rotation")]
        [Usage("jxrotation")]
        public async Task MapRotation(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            string result;
            try
            {
                result = await _httpclient.GetStringAsync($"https://api.mozambiquehe.re/maprotation?auth={_configuration.ApiTrackerKey}");
            }
            catch (Exception)
            {
                await ctx.Channel.SendMessageAsync("Couldn't fetch data");
                return;
            }

            var rotation = ApexService.GetRotation(result);
            var rotationEmbed = ApexService.BuildRotationEmbed(rotation);

            await ctx.RespondAsync(rotationEmbed);
        }
    }
}
