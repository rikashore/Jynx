using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Database.Entities
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
