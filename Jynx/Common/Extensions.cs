using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jynx.Common
{
    public static class Extensions
    {
        public static async Task<string> ParseCodeBlock(this string s, CommandContext ctx, ParseType parseType)
        {
            if (s == null)
            {
                await ctx.Channel.SendMessageAsync("You need to give a code block");
                return null;
            }

            var parser = parseType switch
            {
                ParseType.Lua => "lua",
                ParseType.Cs => "cs",
                _ => ""
            };


            // TODO: Need to make changes to parse system
            var cs1 = s.IndexOf($"```{parser}", StringComparison.OrdinalIgnoreCase) + 3;
            cs1 = s.IndexOf('\n', cs1) + 1;
            var cs2 = s.LastIndexOf("```", StringComparison.OrdinalIgnoreCase);

            if (cs1 == -1 || cs2 == -1)
            {
                await ctx.Channel.SendMessageAsync("You need to wrap the code into a code block.");
                return null;
            }

            var cs = s.Substring(cs1, cs2 - cs1);

            return cs;
        }

        public static string ToFirstUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input[1..]
            };

        public static bool In<T>(this T e, params T[] list)
            => e.In((IEnumerable<T>)list);

        public static bool In<T>(this T e, IEnumerable<T> list)
            => list.Any(l => (e == null && l == null) || (e?.Equals(l) ?? false));

        public static bool NotIn<T>(this T e, params T[] list)
            => !In(e, list);
    }

    public enum ParseType
    {
        Lua,
        Cs,
        Default
    }
}
