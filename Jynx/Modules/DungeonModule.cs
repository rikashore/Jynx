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

namespace Jynx.Modules
{
    public class DungeonModule : BaseCommandModule
    {
        private Random rnd = new Random();

        [Command("dungeon")]
        [Description("begin a dungeon run")]
        public async Task Dungeon(CommandContext ctx)
        {
            var dungeonHandler = new DungeonHandler();

            await dungeonHandler.ProcessDungeon(ctx);
        }
    }
}
