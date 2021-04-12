using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Dungeons.Types
{
    public class DungeonEnemyRoom : IDungeonRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DungeonEnemy Enemy { get; set; }

        public DungeonEnemyRoom(string name, string description, DungeonEnemy enemy)
        {
            Name = name;
            Description = description;
            Enemy = enemy;
        }
    }
}
