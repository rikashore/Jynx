using Jynx.Dungeons.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Dungeons.Utilities
{
    public static class DungeonConstants
    {
        private static readonly Random rand = new Random();

        public static string[] Enemies { get { return enemies; } }

        public static string[] Rooms { get { return rooms; } }

        public static Dictionary<string, DungeonEnemy> DungeonEnemies
        {
            get { return dungeonenemies; }
        }

        public static List<string> RoomDescriptions { get { return roomDescriptions; } }

        public const int MaxDamage = 30;
        public const int MaxHealth = 100;
        public const int HealAmount = 30;

        private static string[] enemies = new string[] { "skeleton", "zombie", "assassin", "warrior" };

        private static string[] rooms = new string[] { "enemy", "loot" };

        private static Dictionary<string, DungeonEnemy> dungeonenemies = new Dictionary<string, DungeonEnemy>
        {
            {
                "skeleton", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Bones", "Ribbs", "Creampuff"
                    },
                    Descriptions = new List<string>
                    {
                        "A pile of bones", "A cream figure", "A lost human"
                    }
                }
            },
            {
                "zombie", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Pack", "Porcupine", "Scuttler"
                    },
                    Descriptions = new List<string>
                    {
                        "A mass of flesh", "raggedly old janitor", "office worker"
                    }
                }
            },
            {
                "assassin", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Cerviel", "Ezio", "Xaphan"
                    },
                    Descriptions = new List<string>
                    {
                        "master of the shadows", "A hidden one", "Master of disguise"
                    }
                }
            },
            {
                "warrior", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Goldcrest", "Death Heart", "Ravenbane"
                    },
                    Descriptions = new List<string>
                    {
                        "soldier of a forgotten time", "fighter most fearsome", "strong-arm"
                    }
                }
            }
        };

        private static List<string> roomDescriptions = new List<string>
        {
            "A cave room with spires from the ceiling",
            "A dark room with cobwebs",
            "A mossy tunnel",
            "A stone carved tunnel",
            "A room of a ancient drawings"
        };
    }
}
