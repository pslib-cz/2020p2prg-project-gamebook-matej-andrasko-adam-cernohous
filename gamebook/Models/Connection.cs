using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Models
{
    public class Connection
    {
        public string Description { get; set; }
        public Places NextPlace { get; set; } // index cílové místnosti
        public string SpecialPlace { get; set; } = null;
        public Places From { get; set; } // odkud vycházíme
    }
}
