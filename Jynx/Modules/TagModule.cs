using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Jynx.Database.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Modules
{
    [Group("tag")]
    [Description("Tag related commands, invoke without a subcommand to search the tag database")]
    public class TagModule : BaseCommandModule
    {
        public TagHelper Tags { private get; set; }

        [GroupCommand]
        public async Task Tag(CommandContext ctx, [RemainingText] string name)
        {
            var tag = await Tags.GetTag(name);

            if (tag == null)
            {
                await ctx.RespondAsync("That tag does not exist!");
                return;
            }

            await ctx.RespondAsync(tag.Content);
        }

        [Command("add")]
        public async Task TagAdd(CommandContext ctx, string tagName, [RemainingText] string tagContent)
        {
            var tag = await Tags.GetTag(tagName);

            if (tag != null)
            {
                await ctx.RespondAsync("A tag with this name already exists");
                return;
            }

            await Tags.AddTag(tagName, tagContent);

            await ctx.RespondAsync($"Tag {tagName} created");
        }

        [Command("del")]
        [Description("Delete a tag")]
        public async Task TagDelete(CommandContext ctx, string tagName)
        {
            var tagResult = await Tags.DeleteTag(tagName);

            if (tagResult == 0)
            {
                await ctx.RespondAsync($"Tag {tagName} does not exist");
                return;
            }

            await ctx.RespondAsync($"Tag {tagName} was successfully deleted");
        }
    }
}
