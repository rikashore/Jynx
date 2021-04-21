using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Dungeons;
using Jynx.Dungeons.Utilities;
using System.Threading.Tasks;
using Jynx.Common;
using Jynx.Common.Attributes;
using Jynx.Database.Helpers;

namespace Jynx.Modules
{
    public class DungeonModule : BaseCommandModule
    {
        public UserHelper userHelper { private get; set; }

        [Command("dungeon")]
        [Description("begin a dungeon run")]
        [Usage("jxdungeon")]
        public async Task Dungeon(CommandContext ctx)
        {
            var dungeonHandler = new DungeonHandler(userHelper);

            await dungeonHandler.ProcessDungeon(ctx);
        }

        [Command("dinfo")]
        [Description("get you current dungeon info")]
        [Usage("jxdinfo")]
        public async Task Gold(CommandContext ctx)
        {
            await ctx.Channel.TriggerTypingAsync();
            
            var goldAmount = await userHelper.GetGold(ctx.Member.Id);
            var healthPotions = await userHelper.GetHealthPotions(ctx.Member.Id);
            var dungeonInfoEmbed = new DiscordEmbedBuilder()
                .WithTitle($"Info for {ctx.Member.Username}")
                .WithColor(JynxCosmetics.JynxColor)
                .WithThumbnail(ctx.Member.AvatarUrl)
                .AddField("Gold", $"{goldAmount}", true)
                .AddField("Health Potions", $"{healthPotions}", true);

            await ctx.RespondAsync(dungeonInfoEmbed);
        }

        [Command("buy")]
        [Description("Buy health potions")]
        [RequireBusinessHours]
        [Usage("jxbuy [amount]")]
        public async Task Buy(CommandContext ctx, int amount = 1)
        {
            var price = DungeonConstants.Price;
            var goldAmount = await userHelper.GetGold(ctx.Member.Id);
            if (goldAmount < price*amount)
            {
                await ctx.RespondAsync($"You do not have enough gold to buy {amount} health potions, you can buy less or fight in the dungeon for more gold");
                return;
            }

            await userHelper.IncrementHealthPotions(ctx.Member.Id, amount);
            await userHelper.DecrementGold(ctx.Member.Id, price * amount);
        }
    }
}
