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

        public static Dictionary<string, DungeonEnemy> DungeonEnemies
        {
            get { return dungeonenemies; }
        }

        public static string[] enemies = new string[] { "skeleton", "zombie", "assassin", "warrior" };

        public static Dictionary<string, DungeonEnemy> dungeonenemies = new Dictionary<string, DungeonEnemy>
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

        public static (string, string) GetEnemyDetails(string descriptionFor)
        {
            var enemy = DungeonEnemies[descriptionFor];
            var name = enemy.Names[rand.Next(enemy.Names.Count)];
            var description = enemy.Descriptions[rand.Next(enemy.Descriptions.Count)];

            return (name, description);
        }

        public static string GetEnemy()
        {
            var index = rand.Next(Enemies.Length);
            return Enemies[index];
        }
    }
}
