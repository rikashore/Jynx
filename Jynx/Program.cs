using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Jynx.Common;
using Jynx.Database;
using Jynx.Database.Helpers;
using Jynx.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Exceptions;
using Jynx.Common.Attributes;

namespace Jynx
{
    class Program
    {
        static void WriteJynxInfo()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.OutputEncoding = new UTF8Encoding();
            Console.WriteLine(JynxCosmetics.JynxAscii);
            Console.ForegroundColor = default;
        }

        static void Main(string[] args)
        {
            var jynx = new Jynx();
            WriteJynxInfo();
            jynx.RunAsync().GetAwaiter().GetResult();
        }
    }

    public class Jynx
    {
        public readonly EventId BotEventId = new EventId(11, "Jynx");
        public DiscordShardedClient Client { get; private set; }
        public string avatar => Client.CurrentUser.AvatarUrl;
        public ServiceProvider Services { get; private set; }
        public Configuration Configuration { get; private set; } = new();

        public async Task RunAsync()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: SystemConsoleTheme.Grayscale)
                .CreateLogger();

            var logFactory = new LoggerFactory().AddSerilog();
            var config = new DiscordConfiguration
            {
                Token = Configuration.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                LoggerFactory = logFactory,
                Intents = DiscordIntents.All
            };

            Client = new DiscordShardedClient(config);

            Services = new ServiceCollection()
                .AddDbContext<JynxContext>(x => 
                    x.UseMySql(Configuration.DbConnection, new MySqlServerVersion(new Version(8, 0 ,21))))
                .AddSingleton(this)
                .AddSingleton<HttpClient>()
                .AddSingleton(Configuration)
                .AddSingleton<TagHelper>()
                .AddSingleton<UserHelper>()
                .BuildServiceProvider();


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = Configuration.Prefixes,
                EnableMentionPrefix = true,
                EnableDms = false,
                Services = Services
            };

            var interactivityConfig = new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5)
            };
            
                        
            Client.Logger.LogInformation("Running version {version}", Configuration.Version);

             await Client.UseCommandsNextAsync(commandsConfig);
             await Client.UseInteractivityAsync(interactivityConfig);

            foreach (var client in Client.ShardClients)
            {
                client.Value.MessageCreated += OnMessage;
                client.Value.Ready += OnClientReady;
                
                client.Value.GetCommandsNext().RegisterCommands(Assembly.GetExecutingAssembly());
                client.Value.GetCommandsNext().SetHelpFormatter<JynxHelp>();

                client.Value.GetCommandsNext().CommandErrored += OnError;
            }
            
            await Client.StartAsync();

            await Task.Delay(-1);

        }

        private Task OnMessage(DiscordClient sender, MessageCreateEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                string[] words = { "jynx", "i", "need", "help" };
                var msg = e.Message.Content.ToLowerInvariant();

                if (words.All(x => msg.Contains(x.ToLowerInvariant())))
                {
                    var helpContext = sender.GetCommandsNext().CreateFakeContext(sender.CurrentUser, e.Channel, e.Message.Content, "jx", sender.GetCommandsNext().RegisteredCommands["help"]);
                    await sender.GetCommandsNext().ExecuteCommandAsync(helpContext);
                }
            });

            return Task.CompletedTask;
        }

        private async Task OnError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);
            
            var failedChecks = ((ChecksFailedException) e.Exception).FailedChecks;
            foreach (var failedCheck in failedChecks)
            {
                if (failedCheck is RequireBusinessHoursAttribute)
                    await e.Context.RespondAsync($"Shops closed {e.Context.Member.Username}, come again between 9 AM and 8 PM");
            }
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            var activity = new DiscordActivity("jx help", ActivityType.Playing);
            Client.UpdateStatusAsync(activity);

            return Task.CompletedTask;
        }
    }
}
