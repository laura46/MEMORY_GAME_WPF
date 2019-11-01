﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryProject.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int? Score { get; set; }
        public Dictionary<string,int> Powerups { get; set; }

    }
}
