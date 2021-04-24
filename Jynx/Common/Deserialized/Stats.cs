namespace Jynx.Common.Deserialized
{
    public class Rank
    {
        public int rankScore { get; set; }
        public string rankName { get; set; }
        public int rankDiv { get; set; }
        public int ladderPosPlatform { get; set; }
        public string rankImg { get; set; }
        public string rankedSeason { get; set; }
    }

    public class Global
    {
        public string name { get; set; }
        public long uid { get; set; }
        public string avatar { get; set; }
        public string platform { get; set; }
        public int level { get; set; }
        public Rank rank { get; set; }
    }

    public class Kills
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Kd
    {
        public int value { get; set; }
        public string name { get; set; }
    }

    public class Total
    {
        public Kills kills { get; set; }
        public Kd kd { get; set; }
    }

    public class Stats
    {
        public Global global { get; set; }
        public Total total { get; set; }
    }
}