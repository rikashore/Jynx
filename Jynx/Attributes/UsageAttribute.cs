using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UsageAttribute : Attribute
    {
        private string usage;

        public UsageAttribute(string usage)
        {
            this.usage = usage;
        }

        public virtual string Usage
        {
            get { return usage; }
        }
    }
}
