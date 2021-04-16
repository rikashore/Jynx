using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Jynx.Common;
using Jynx.Dungeons.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jynx.Database.Helpers;

namespace Jynx.Dungeons
{
    public class DungeonHandler
    {
        public UserHelper userHelper { private get; set; }
        private readonly Random _rnd = new();

        public DungeonHandler(UserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public async Task ProcessDungeon(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            int playerHealth = 100;
            int healthPotions = await userHelper.GetHealthPotions(ctx.Member.Id);
            bool ranAway = false;

            while (true)
            {
                int enemyHealth = _rnd.Next(DungeonConstants.MaxHealth);
                var enemy = DungeonMethods.GetEnemy();
                var (name, s) = DungeonMethods.GetEnemyDetails(enemy);
                string description = DungeonMethods.GetRandomRoomDescription();

                var message = DungeonMethods.BuildRoomMessage(description, name, s);

                await ctx.Channel.SendMessageAsync(message);

                while(enemyHealth > 0)
                {
                    var msg = await ctx.Channel.SendMessageAsync($"**Your HP:** {playerHealth}\n**Enemy's HP:** {enemyHealth}\nWhat would you like to do?\n> 1. Attack\n> 2. Heal\n> 3. Run!");

                    var input = await interactivity.WaitForMessageAsync(x => x.ChannelId == ctx.Channel.Id && x.Author.Id == ctx.Member.Id);
                    var choice = input.Result.Content.ToLower();

                    if(choice == "1")
                    {
                        int damageDealt = DungeonMethods.GetDamage();
                        int damageTaken = DungeonMethods.GetDamage();

                        enemyHealth -= damageDealt;
                        playerHealth -= damageTaken;

                        await ctx.Channel.SendMessageAsync($"You strike {name} for {damageDealt} damage\nYou received {damageTaken} damage in retaliation");

                        if(playerHealth < 1)
                        {
                            await ctx.Channel.SendMessageAsync("You have taken too much damage, you are too weak to go on");
                            break;
                        }
                    }
                    else if (choice == "2")
                    {
                        if (healthPotions > 0)
                        {
                            if(playerHealth < 70)
                            {
                                playerHealth += DungeonConstants.HealAmount;
                                await userHelper.DecrementHealthPotions(ctx.Member.Id);

                                await ctx.Channel.SendMessageAsync($"You heal yourself for {DungeonConstants.HealAmount}. Your current HP is {playerHealth}\nYou have {healthPotions} health potions left");
                            }
                            else
                            {
                                await ctx.Channel.SendMessageAsync("Your health is too high to heal");
                            }
                        }
                        else
                        {
                            await ctx.Channel.SendMessageAsync("You have no health potions, Defeat enemies for a chance at getting a health potion");
                        }
                    }
                    else if (choice == "3")
                    {
                        await ctx.Channel.SendMessageAsync($"You ran away from {name}!");
                        ranAway = true;
                        break;
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync("Invalid command!");
                        continue;
                    }
                }

                if (ranAway)
                    continue;

                if(playerHealth < 1)
                {
                    await ctx.Channel.SendMessageAsync("You limp out of the dungeon, weak from your wounds");
                    break;
                }

                if (_rnd.Next(1, 101) > 65)
                {
                    await userHelper.IncrementHealthPotions(ctx.Member.Id);
                    var currentHealthPots = await userHelper.GetHealthPotions(ctx.Member.Id);
                    await ctx.Channel.SendMessageAsync($"{name} dropped a health potion\nYou now have {currentHealthPots} left");
                }
                else
                {
                    int goldLoot = DungeonMethods.GetLootAmount();
                    await userHelper.IncrementGold(ctx.Member.Id, goldLoot);
                    await ctx.Channel.SendMessageAsync($"{name} dropped {goldLoot} gold.");
                }

                await ctx.Channel.SendMessageAsync("What would you like to do now?\n> 1. Continue\n> 2. Exit dungeon");

                var decision = await interactivity.WaitForMessageAsync(x => x.ChannelId == ctx.Channel.Id && x.Author.Id == ctx.Member.Id);
                var decisionChoice = decision.Result.Content.ToLower();

                while(decisionChoice.NotIn("1", "2"))
                {
                    await ctx.Channel.SendMessageAsync("Invalid command!");
                    decision = await interactivity.WaitForMessageAsync(x => x.ChannelId == ctx.Channel.Id && x.Author.Id == ctx.Member.Id);
                    decisionChoice = decision.Result.Content.ToLower();
                }

                if (decisionChoice == "1")
                    await ctx.Channel.SendMessageAsync("You continue through the dungeon");
                else if (decisionChoice == "2")
                {
                    await ctx.Channel.SendMessageAsync("You exit the dungeon, victorious");
                    break;
                }
            }
        }
    }
}
