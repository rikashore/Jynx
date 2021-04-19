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
using Jynx.Common.Attributes;

namespace Jynx
{
    public class JynxHelp : BaseHelpFormatter
    {
        private DiscordEmbedBuilder MessageBuilder { get; }
        private readonly Configuration _configuration = new Configuration();
        
        private bool _withCommandCalled;

        public JynxHelp(CommandContext ctx) : base(ctx)
        {
            this.MessageBuilder = new DiscordEmbedBuilder()
                .WithTitle("Jynx")
                .WithColor(JynxCosmetics.JynxColor);

        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            _withCommandCalled = true;
            if (command is CommandGroup group)
            {
                this.MessageBuilder.WithTitle(command.Name);
                this.MessageBuilder.AddField("Description", group.Description ?? "none");
                this.MessageBuilder.AddField("Commands", $"> {string.Join("\n> ", group.Children.Select(x => x.Name))}");
            }
            else
            {
                string aliases = string.Join(", ", command.Aliases);

                if (string.IsNullOrWhiteSpace(aliases))
                {
                    aliases = "none";
                }

                this.MessageBuilder.WithTitle(command.Name);
                this.MessageBuilder.AddField("Command Group", command.Parent == null ? "Not part of a command group" : command.Parent.Name);
                this.MessageBuilder.AddField("Description", command.Description ?? "none");
                this.MessageBuilder.AddField("Aliases", aliases);

                var usage = (UsageAttribute)command.CustomAttributes.SingleOrDefault(x => x is UsageAttribute);

                this.MessageBuilder.AddField("Usage", usage == null ? "none" : usage.Usage);
            }

            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            if (_withCommandCalled)
            {
                return this;
            }

            this.MessageBuilder.AddField("Commands and Command Groups", $"> {string.Join("\n> ", subcommands.Select(xc => xc.Name))}")
                .WithDescription($"Type {string.Join('/', _configuration.Prefixes.Select(Formatter.InlineCode))} help [commandname/commandgroup] to get more info on a particular command or command group");
            return this;

        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(null, this.MessageBuilder.Build());
        }
    }
}
