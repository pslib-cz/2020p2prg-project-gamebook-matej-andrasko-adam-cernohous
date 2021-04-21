using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Models
{
    public class GameState
    {
        public Places Location { get; set; }
        public int HP { get; set; }
        public List<Item> Items { get; set; }
        
    }
}
