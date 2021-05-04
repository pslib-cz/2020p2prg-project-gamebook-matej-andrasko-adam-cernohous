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
        
        
        //Místnosti s možností sběru peněz
        public string MoneyPlace { get; set; }
        public string Moneydesc { get; set; }
        public int MoneyGet { get; set; }

        //Možnost nákupu
        public string BuyingPlace { get; set; }
        public int HpUp { get; set; }
        public int Cost { get; set; }

        public Places From { get; set; } // odkud vycházíme
    }
}
