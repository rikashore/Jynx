using Jynx.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Database.Helpers
{
    public class UserHelper
    {
        public JynxContext JynxContext { get; set; }

        public UserHelper(JynxContext context)
        {
            JynxContext = context;
        }

        public async Task IncrementHealthPotions(ulong id)
        {
            var user = await JynxContext.Users
                .FindAsync(id);

            user.HealthPotions++;

            await JynxContext.SaveChangesAsync();
        }

        public async Task IncrementGold(ulong id, int amount)
        {
            var user = await JynxContext.Users
                .FindAsync(id);

            if (user == null)
                JynxContext.Add(new User { Id = id, GoldAmount = amount });
            else
                user.GoldAmount += amount;

            await JynxContext.SaveChangesAsync();
        }

        public async Task<int> GetHealthPotions(ulong id)
        {
            var user = await JynxContext.Users
                .FindAsync(id);

            if (user == null)
            {
                JynxContext.Add(new User { Id = id, HealthPotions = 2 });
                await JynxContext.SaveChangesAsync();
                return 2;
            }

            else
                return user.HealthPotions;
        }

        public async Task<int> GetGold(ulong id)
        {
            var user = await JynxContext.Users
                .FindAsync(id);

            return user?.GoldAmount ?? 0;
        }

        public async Task DecrementHealthPotions(ulong id)
        {
            var user = await JynxContext.Users
                .FindAsync(id);

            user.HealthPotions--;

            await JynxContext.SaveChangesAsync();
        }

        public async Task<int> DecrementGold(ulong id, int amount)
        {
            var user = await JynxContext.Users
                .FindAsync(id);

            if (user == null)
                return 0;
            else
                user.GoldAmount -= amount;

            await JynxContext.SaveChangesAsync();

            return 1;
        }
    }
}
