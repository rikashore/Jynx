using System;

namespace Jynx.Common.Attributes
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
