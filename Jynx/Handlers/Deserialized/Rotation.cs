using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Handlers.Deserialized
{
    public class Current
    {
        public int start { get; set; }
        public int end { get; set; }
        public string readableDate_start { get; set; }
        public string readableDate_end { get; set; }
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
        public string readableDate_start { get; set; }
        public string readableDate_end { get; set; }
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
