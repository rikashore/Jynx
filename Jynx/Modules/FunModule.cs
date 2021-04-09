using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Modules
{
    public class FunModule : BaseCommandModule
    {
        [Command("choose")]
        [Description("chooses stuff")]
        [Aliases("choice")]
        [Usage("jxchoose [options split by '|']")]
        public async Task Choose(CommandContext ctx, [RemainingText] string text)
        {
            if (text == null)
            {
                await ctx.RespondAsync("I need more choices");
                return;
            }

            string[] choices = text.Split('|');

            if (choices.Length < 2 || choices == null)
            {
                await ctx.RespondAsync("I need more choices");
                return;
            }

            var rng = new Random();

            var index = rng.Next(choices.Length);

            var choice = choices[index];

            await ctx.RespondAsync($"I choose {choice}");
        }

        [Group("color")]
        public class ColorCommands : BaseCommandModule
        {
            [Command("hex")]
            [Description("make a random color in hex format")]
            public async Task RandomColor(CommandContext ctx, int amount = 1)
            {
                int i = 1;
                while (i <= amount)
                {
                    var random = new Random();
                    var color = string.Format("#{0:X6}", random.Next(0x1000000));

                    var colorEmbed = new DiscordEmbedBuilder()
                        .WithDescription(color)
                        .WithColor(new DiscordColor(color));

                    await ctx.RespondAsync(colorEmbed);
                    i++;
                }
            }

            [Command("rgb")]
            [Description("make a random color in rgb format")]
            public async Task RandomRgbColor(CommandContext ctx, int amount = 1)
            {
                int i = 1;
                while (i <= amount)
                {
                    var buffer = new byte[3];

                    var random = new Random();
                    random.NextBytes(buffer);

                    var colorEmbed = new DiscordEmbedBuilder()
                        .WithDescription(string.Join(" ", buffer))
                        .WithColor(new DiscordColor(buffer[0], buffer[1], buffer[2]));

                    await ctx.RespondAsync(colorEmbed);
                    i++;
                }
            }
        }
    }
}
