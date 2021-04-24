using System.Collections.Generic;

namespace Jynx.Common
{
    public static class JynxFormatter
    {
        public static string NewlineQuote<T>(this T[] list)
            => NewlineQuote((IEnumerable<T>)list);

        public static string NewlineQuote<T>(this IEnumerable<T> list)
            => $"> {string.Join("\n> ", list)}";
    }
}
