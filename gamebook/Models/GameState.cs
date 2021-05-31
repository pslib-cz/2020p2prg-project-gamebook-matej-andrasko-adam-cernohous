using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Models
{
    public class GameState
    {
        public Places Location { get; set; }
        public int HP { get; set; } = 5;
        public Characters Character { get; set; }
        public int Money { get; set; }
        public int bossHP = 10;
        public bool BossAlive { get; set; } = true;
        public Places Check { get; set; }
        public Places Current { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
        
    }
}
