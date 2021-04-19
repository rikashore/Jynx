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
                        "A pile of bones", "A cream figure", "A lost human", "grey skull", "organ-less walker",
                        "empty husk", "white walker", "bone walker", "soul-less human", "soul-less husk",
                        "the ribbed", "the puffy", "red bones", "dried whiteness", "captain of the bones",
                        "thick skulled", "callous fingers", "the handless", "the legless", "the headless"
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
                        "mass of flesh", "raggedly old janitor", "office worker", "stubbed toe", "singed body",
                        "knee-capped", "twisted knees", "mutilated arm", "eyeless", "gore-some",
                        "grey eyed", "red eyed", "black eyed", "falling skin", "mud-caked",
                        "clean haired", "nocturnal", "four digits", "no digits", "half skull"
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
                        "master of the shadows", "A hidden one", "master of disguise", "shadow warrior", "warrior of the night",
                        "night bane", "moonlit skulker", "moonlit striker", "moonlit scythe", "silver blade",
                        "torturous killer", "slow killer", "blade master", "silent walker", "moonwalker",
                        "sun warrior", "hooded warrior", "cloaked scythe", "cloaked skulker", "masked warrior"
                    }
                }
            },
            {
                "warrior", new DungeonEnemy
                {
                    Names = new List<string>
                    {
                        "Goldcrest", "Death Heart", "Ravenbane", "Shepherd", "Wolfbrow", 
                        "Grand Mane", "Silverrage", "Behemoth", "Moltensword", "Ragehthorn",
                        "Doomhair", "Rock Gaze", "Lightcrest", "Honorbound", "Hellscream", 
                        "Helleye", "Darktongue", "Lightchaser", "Fusesworn", "Sharpblood"
                    },
                    Descriptions = new List<string>
                    {
                        "soldier of a forgotten time", "fighter most fearsome", "strong-arm", "spider's bane", "terror of the day",
                        "silver-haired warrior", "red-haired warrior", "tank", "thorn in the side", "sword of fire",
                        "sword of ice", "bound to light", "bound to fire", "bound to earth", "blood of iron",
                        "hard gaze", "soft gaze", "silver tongue", "hiss tongue", "animal herder"
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
                    },
                    Descriptions = new List<string>
                    {
                        "dragon slayer", "bane of dragons", "slayer of beasts", "champion of the olympics", "strongest person in the west",
                        "divine being", "light walker", "devout worker", "angelic voice", "cutest person of the east",
                        "warrior of the north", "warrior of the east", "fastest in the south", "terror-ful person", "big stomper",
                        "selfish being", "darkness warrior", "messenger of the north", "messenger of the south", "silent warrior"
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
