namespace Jynx.Handlers.Deserialized
{
    public class Current
    {
        public int start { get; set; }
        public int end { get; set; }
        public string ReadableDateStart { get; set; }
        public string ReadableDateEnd { get; set; }
        public string map { get; set; }
        public int DurationInSecs { get; set; }
        public int DurationInMinutes { get; set; }
        public int remainingSecs { get; set; }
        public int remainingMins { get; set; }
        public string remainingTimer { get; set; }
    }

    public class Next
    {
        public int start { get; set; }
        public int end { get; set; }
        public string ReadableDateStart { get; set; }
        public string ReadableDateEnd { get; set; }
        public string map { get; set; }
        public int DurationInSecs { get; set; }
        public int DurationInMinutes { get; set; }
    }

    public class Rotation
    {
        public Current current { get; set; }
        public Next next { get; set; }
    }
}
