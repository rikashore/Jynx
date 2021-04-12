using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Dungeons.Types
{
    public class DungeonLootRoom : IDungeonRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private readonly Random rnd = new Random();

        public DungeonLootRoom(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public LootType GetLootType()
        {
            int index = rnd.Next(0, 2);

            LootType lootType = index switch
            {
                0 => LootType.Gold,
                1 => LootType.HealthPotion,
                _ => LootType.Gold
            };

            return lootType;
        }

        public int GetLootAmount(LootType lootType)
        {
            int returnAmount = lootType switch
            {
                LootType.Gold => rnd.Next(10, 31),
                LootType.HealthPotion => rnd.Next(1, 4),
                _ => throw new ArgumentException("Invalid LootType")
            };

            return returnAmount;
        }
    }
}
