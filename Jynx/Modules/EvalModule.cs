using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Jynx.Common;
using Jynx.Common.Attributes;

namespace Jynx.Modules
{
    [Group("eval")]
    public class EvalModule : BaseCommandModule
    {
        [Command("lua")]
        [Description("Eval some lua")]
        [Usage("jxeval lua [code block for execution]")]
        public async Task EvalLua(CommandContext ctx, [RemainingText] string code)
        {
            if (!JynxExtensions.TryParseCodeBlock(code, out code))
            {
                await ctx.RespondAsync("You need to wrap the code in a code block");
                return;
            }
            
            var cs = code.ParseCodeBlock();

            try
            {
                Script script = new Script();

                var res = script.DoString(cs);

                object resString = res.Type switch
                {
                    DataType.String => res.String,
                    DataType.Number => res.Number,
                    DataType.Boolean => res.Boolean,
                    DataType.Function => res.Function,
                    DataType.Nil => "null",
                    DataType.Void => "null",
                    DataType.Table => res.Table,
                    DataType.Tuple => res.Tuple,
                    DataType.UserData => "null",
                    DataType.Thread => "null",
                    DataType.ClrFunction => res.Callback.Name,
                    DataType.TailCallRequest => res.UserData.Descriptor.Name,
                    DataType.YieldRequest => "null",
                    _ => "null"
                };

                var resEmbed = new DiscordEmbedBuilder()
                    .WithTitle("Evaluation Success")
                    .WithDescription($"Value\n```lua\n{resString}\n```")
                    .WithColor(JynxCosmetics.JynxColor)
                    .Build();

                await ctx.RespondAsync(resEmbed);
            }
            catch (Exception e)
            {
                if(e is ScriptRuntimeException runtimeException)
                {
                    var errEmbed = new DiscordEmbedBuilder()
                        .WithTitle("An error occurred, Runtime Exception")
                        .WithDescription($"```lua\n{runtimeException.DecoratedMessage}\n```")
                        .WithColor(JynxCosmetics.JynxColor)
                        .Build();

                    await ctx.RespondAsync(errEmbed);
                }
                else if(e is SyntaxErrorException syntaxException)
                {
                    var errEmbed = new DiscordEmbedBuilder()
                        .WithTitle($"An error occurred, Syntax Exception")
                        .WithDescription($"```lua\n{syntaxException.DecoratedMessage}\n```")
                        .WithColor(JynxCosmetics.JynxColor)
                        .Build();

                    await ctx.RespondAsync(errEmbed);
                }
            }

        }
    }
}
