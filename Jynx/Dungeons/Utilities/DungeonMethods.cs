using Jynx.Dungeons.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Dungeons.Utilities
{
    public static class DungeonMethods
    {
        private static readonly Random rand = new Random();

        public static (string, string) GetEnemyDetails(string descriptionFor)
        {
            var enemy = DungeonConstants.DungeonEnemies[descriptionFor];
            var name = enemy.Names[rand.Next(enemy.Names.Count)];
            var description = enemy.Descriptions[rand.Next(enemy.Descriptions.Count)];

            return (name, description);
        }

        public static string GetRandomRoomDescription()
        {
            var descriptions = DungeonConstants.RoomDescriptions;
            var description = descriptions[rand.Next(DungeonConstants.RoomDescriptions.Count)];

            return description;
        }

        public static string GetEnemy()
        {
            var index = rand.Next(DungeonConstants.Enemies.Length);
            return DungeonConstants.Enemies[index];
        }

        public static int GetDamage()
        {
            return rand.Next(1, DungeonConstants.MaxDamage + 1);
        }

        public static string BuildRoomMessage(string roomName, string roomDescription)
            => $"You have entered {roomName}, {roomDescription}";

        public static string BuildRoomMessage(string roomDescription, string enemyName, string enemyDescription)
            => $"You enter {roomDescription}\nYou encounter {enemyName}, {enemyDescription}";

        public static int GetLootAmount()
        {
            int returnAmount = rand.Next(10, 37);
            return returnAmount;
        }
    }
}
