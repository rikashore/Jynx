﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx.Dungeons.Types
{
    public interface IDungeonRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
