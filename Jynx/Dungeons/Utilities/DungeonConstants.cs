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
        public const int Price = 100;

        public static readonly string[] Enemies = { "skeleton", "zombie", "assassin", "warrior" };

        public static readonly Dictionary<string, DungeonEnemy> DungeonEnemies = new()
        {
            {
                "skeleton", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Bones", "Ribbs", "Creampuff", "McRibs", "Sans", "Death Bridges", "Rot", "Kill Gates", "Sarumarrow", "Bona Lisa",
                        "Scary Poppins", "Caius", "Crimson", "Daray", "Abel", "Dry Bones", "Casper", "Bonehead", "Skully", "Corbin"
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
                        "Pack", "Porcupine", "Scuttler", "Ticker", "Glazer","Screecher", "Camo", "Burner", "Tainted", "Spurter", 
                        "Grunge", "Sponger", "Recollector", "Whistler", "Crusher", "Hacksaw", "Sloucher", "Lanky", "Hunter", "Driver"
                    },
                    Descriptions = new List<string>
                    {
                        "mass of flesh", "raggedly old janitor", "office worker"
                    }
                }
            },
            {
                "assassin", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Cerviel", "Ezio", "Xaphan", "Deadstain", "Redsaw", "Golden Mark", "Brass", "Dillema", "Padlock", "Dawn",
                        "The Blue Shadow", "The Still Whisper", "Scarlet Eye", "Ghostshade", "The Nimble Rock",
                        "The Steel Shade", "Rapid Phoenix", "Tranquil Child", "Sapphire Devil", "Ironsign"
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
                        "Goldcrest", "Death Heart", "Ravenbane", "Shepherd", "Wolfbrow", "Grand Mane", "Silverrage", "Behemoth", "Moltensword", "Ragehthorn",
                        "Doomhair", "Rock Gaze", "Lightcrest", "Honorbound", "Hellscream", "Helleye", "Darktongue", "Lightchaser", "Fusesworn", "Sharpblood"
                    },
                    Descriptions = new List<string>
                    {
                        "soldier of a forgotten time", "fighter most fearsome", "strong-arm"
                    }
                }
            },
            {
                "knight", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Water the whisper", "Mousse the protector", "Gabell the weak", "Barda the creature", "Dreu the amazing",
                        "Jeremimum the undefeated", "Gylbarde the selfish", "Tim the silence", "Rankin the swift", "Jacobus the messenger",
                        "Melisenda the silence", "Orella the giant", "Gynuara the angel", "Magdalene the cute", "Avis the amazing",
                        "Iseut the divine", "Richardyne of the light", "Oriolda of the Spring", "Libet the horrific", "Ermyntrude the relentless"
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
