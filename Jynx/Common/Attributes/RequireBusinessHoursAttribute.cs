using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Jynx.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireBusinessHoursAttribute : CheckBaseAttribute
    {
        private static readonly DateTime Now = DateTime.Now;
        private readonly DateTime _startingWorkHour = new DateTime(Now.Year, Now.Month, Now.Day, 9, 0, 0);
        private readonly DateTime _endingWorkHour = new DateTime(Now.Year, Now.Month, Now.Day, 20, 0, 0);
        
        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            return Task.FromResult(Now > _startingWorkHour && Now < _endingWorkHour);
        }
    }
}