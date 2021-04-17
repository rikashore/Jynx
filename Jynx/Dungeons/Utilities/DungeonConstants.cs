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
        public const int MaxDamage = 30;
        public const int MaxHealth = 100;
        public const int HealAmount = 30;

        public static readonly string[] Enemies = { "skeleton", "zombie", "assassin", "warrior" };

        public static readonly Dictionary<string, DungeonEnemy> DungeonEnemies = new()
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

        public static readonly List<string> RoomDescriptions = new()
        {
            "A cave room with spires from the ceiling",
            "A dark room with cobwebs",
            "A mossy tunnel",
            "A stone carved tunnel",
            "A room of a ancient drawings"
        };
    }
}
