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
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
        public readonly EventId BotEventId = new EventId(11, "Disaris");
        public DiscordClient Client { get; private set; }
        public int latency => Client.Ping;
        public string avatar => Client.CurrentUser.AvatarUrl;
        public InteractivityExtension Interactivity { get; private set; }
        public ServiceCollection Services { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

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

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
            Client.MessageCreated += OnMessage;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5)
            });

            var services = new ServiceCollection()
                .AddDbContext<JynxContext>(x => 
                    x.UseMySql(Configuration.DbConnection, new MySqlServerVersion(new Version(8, 0 ,21))))
                .AddSingleton(this)
                .AddSingleton<HttpClient>()
                .AddSingleton(Configuration)
                .AddSingleton<TagHelper>()
                .AddSingleton<UserHelper>()
                .AddJynxServices()
                .BuildServiceProvider();


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = Configuration.Prefixes,
                EnableMentionPrefix = true,
                EnableDms = false,
                Services = services
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.SetHelpFormatter<JynxHelp>();

            Commands.RegisterCommands(Assembly.GetExecutingAssembly());

            Commands.CommandErrored += OnError;

            Client.Logger.LogInformation("Running version {version}", Configuration.Version);

            await Client.ConnectAsync();

            await Task.Delay(-1);

        }

        private Task OnMessage(DiscordClient sender, MessageCreateEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                string[] words = { "jynx", "i", "need", "help" };
                string[] negation = { "dont", "do not", "dont" };
                var msg = e.Message.Content.ToLower();

                if (words.All(x => msg.Contains(x.ToLower())) && !negation.Any(msg.Contains))
                {
                    var helpContext = Commands.CreateFakeContext(sender.CurrentUser, e.Channel, e.Message.Content, "jx", Commands.RegisteredCommands["help"]);
                    await Commands.ExecuteCommandAsync(helpContext);
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
