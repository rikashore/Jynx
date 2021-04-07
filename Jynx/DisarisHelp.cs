using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using Jynx.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx
{
    public class JynxHelp : BaseHelpFormatter
    {
        private DiscordEmbedBuilder MessageBuilder { get; }

        private bool WithCommandCalled;

        public JynxHelp(CommandContext ctx) : base(ctx)
        {
            this.MessageBuilder = new DiscordEmbedBuilder()
                .WithTitle("Jynx")
                .WithColor(JynxCosmetics.JynxColor);

        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            WithCommandCalled = true;
            if (command is CommandGroup)
            {
                this.MessageBuilder.WithTitle(command.Name);
                var cmd = (CommandGroup)command;
                this.MessageBuilder.AddField("Description", cmd.Description ?? "none");
                this.MessageBuilder.AddField("Commands", $"> {string.Join("\n> ", cmd.Children.Select(x => x.Name))}");
            }
            else
            {
                string aliases = string.Join(", ", command.Aliases);

                if (string.IsNullOrWhiteSpace(aliases))
                {
                    aliases = "none";
                }

                this.MessageBuilder.WithTitle(command.Name);
                this.MessageBuilder.AddField("Description", command.Description ?? "none");
                this.MessageBuilder.AddField("Aliases", aliases);
            }

            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            if (WithCommandCalled)
            {
                return this;
            }
            else
            {
                this.MessageBuilder.AddField("Commands and Command Groups", $"> {string.Join("\n> ", subcommands.Select(xc => xc.Name))}")
                    .WithDescription("Type `ds help [commandname/commandgroup]` to get more info on a particular command or command group");
                return this;
            }

        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(null, this.MessageBuilder.Build());
        }
    }
}
