using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Dungeons.Types;
using Jynx.Dungeons.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Modules
{
    public class DungeonModule : BaseCommandModule
    {
        private Random rnd = new Random();

        [Command("dungeon")]
        [Description("begin a dungeon run")]
        public async Task Dungeon(CommandContext ctx)
        {
            var name = DungeonConstants.GetEnemy();

            (string name, string description) details = DungeonConstants.GetEnemyDetails(name);

            var enemyEmbed = new DiscordEmbedBuilder()
                .WithTitle($"{details.name}, {details.description}");

            await ctx.RespondAsync(enemyEmbed);
        }
    }
}
