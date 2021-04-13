using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Common
{
    public static class Formatter
    {
        public static string InlineCode(string s)
            => $"`{s}`";
    }
}
