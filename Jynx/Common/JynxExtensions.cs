using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jynx.Common
{
    public static class JynxExtensions
    {
        public static string ParseCodeBlock(this string s)
        {
            var cs1 = s.IndexOf($"```", StringComparison.OrdinalIgnoreCase);
            cs1 = s.IndexOf('\n', cs1) + 1;
            var cs2 = s.LastIndexOf("```", StringComparison.OrdinalIgnoreCase);
            var cs = s.Substring(cs1, cs2 - cs1);

            return cs;
        }

        public static bool TryParseCodeBlock(string s, out string code)
        {
            code = null;
            
            var cs1 = s.IndexOf($"```", StringComparison.OrdinalIgnoreCase);
            if (cs1 == -1)
                return false;
            
            cs1 = s.IndexOf('\n', cs1) + 1;
            var cs2 = s.LastIndexOf("```", StringComparison.OrdinalIgnoreCase);

            if (cs1 == -1 || cs2 == -1)
                return false;

            code = s.Substring(cs1, cs2 - cs1);
            return true;
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
}
