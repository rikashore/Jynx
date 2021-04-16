using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Dungeons;
using Jynx.Dungeons.Types;
using Jynx.Dungeons.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jynx.Database.Helpers;

namespace Jynx.Modules
{
    public class DungeonModule : BaseCommandModule
    {
        public UserHelper userHelper { private get; set; }

        [Command("dungeon")]
        [Description("begin a dungeon run")]
        public async Task Dungeon(CommandContext ctx)
        {
            var dungeonHandler = new DungeonHandler(userHelper);

            await dungeonHandler.ProcessDungeon(ctx);
        }

        [Command("gold")]
        [Description("get you current gold")]
        public async Task Gold(CommandContext ctx)
        {
            var goldAmount = await userHelper.GetGold(ctx.Member.Id);
            await ctx.RespondAsync($"You currently have {goldAmount} gold pieces");
        }
    }
}
