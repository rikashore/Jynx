using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Database.Entities
{
    public class User
    {
        public ulong Id { get; set; }
        public int GoldAmount { get; set; }
        public int HealthPotions { get; set; }
    }
}
