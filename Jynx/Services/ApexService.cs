using System;
using DSharpPlus.Entities;
using Jynx.Common;
using Jynx.Common.Attributes;
using Jynx.Common.Deserialized;
using Newtonsoft.Json;

namespace Jynx.Services
{
    [JynxService]
    public class ApexService
    {
        public Stats GetPlayerStats(string response)
        {
            Stats playerStats = JsonConvert.DeserializeObject<Stats>(response);
            return playerStats;
        }

        public DiscordEmbed[] BuildStatEmbeds(Stats stats)
        {
            var statEmbed = new DiscordEmbedBuilder()
                .WithTitle($"Stats for {stats.global.name}")
                .WithColor(JynxCosmetics.JynxColor)
                .WithThumbnail(stats.global.avatar)
                .AddField("Platform", stats.global.platform, true)
                .AddField("Level", stats.global.level.ToString(), true);

            if (stats.total.kills == null)
                statEmbed.AddField("Kills", "couldn't fetch that", true);
            else
                statEmbed.AddField("Kills", stats.total.kills.value == -1 ? "couldn't fetch that" : stats.total.kills.value.ToString(), true);

            if (stats.total.kd == null)
                statEmbed.AddField("K/D", "couldn't fetch that", true);
            else
                statEmbed.AddField("K/D", stats.total.kd.value == -1 ? "couldn't fetch that" : stats.total.kd.value.ToString(), true);

            statEmbed.Build();

            var rankEmbed = new DiscordEmbedBuilder()
                .WithTitle("Ranked info")
                .WithColor(JynxCosmetics.JynxColor)
                .WithThumbnail(stats.global.rank.rankImg)
                .AddField("Rank", stats.global.rank.rankName, true)
                .AddField("Division", stats.global.rank.rankDiv.ToString(), true)
                .AddField("Points", stats.global.rank.rankScore.ToString(), true)
                .Build();

            return new[] { statEmbed, rankEmbed };
        }

        public Rotation GetRotation(string response)
        {
            Rotation rotationInfo = JsonConvert.DeserializeObject<Rotation>(response);
            return rotationInfo;
        }

        public DiscordEmbed BuildRotationEmbed(Rotation rotation)
        {
            var rotationEmbed = new DiscordEmbedBuilder()
                .WithTitle("Map rotation for Apex Legends")
                .WithColor(JynxCosmetics.JynxColor)
                .WithTimestamp(DateTime.Now)
                .AddField("Current Map", rotation.current.map)
                .AddField("Started at", rotation.current.ReadableDateStart)
                .AddField("Will end at", rotation.current.ReadableDateEnd)
                .AddField("Next Map", rotation.next.map)
                .AddField("Starts at", rotation.next.ReadableDateStart)
                .AddField("Duration", $"{rotation.next.DurationInMinutes} mins")
                .Build();

            return rotationEmbed;
        }
    }
}