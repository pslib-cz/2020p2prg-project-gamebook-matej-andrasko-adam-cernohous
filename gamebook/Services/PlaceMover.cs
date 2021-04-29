using gamebook.Exceptions;
using gamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Services
{
    public class PlaceMover : IPlaceMover
    {
        private readonly List<Location> _locations = new()
        {
            new Location { Title = "Byt", Description = "Nacházíš se ve svém bytě, který už jsi neuklízel roky.", Pic = "interier.jpg" },
            new Location { Title = "KFC", Description = "Tvůj džob...pokaždé co sem přijdeš, cítíš ten oder přepáleného tuku.", Pic="kfc.png" },
            new Location { Title = "FGN", Description = "FUJ! Takovej libereckej chánov!", Pic = "fugnerova_in_night.png" },
            new Location { Title = "Trafika", Description = "Stará bába prodává nejlevnější cíga ve městě." },
            new Location { Title = "LIDL", Description = "Pokladna 4 VOLNÁÁÁ" },
            new Location { Title = "Náměstí", Description = "Tady na námku dělaj ten nejlepší kebab." },
            new Location { Title = "Army Shop", Description = "Jak moje máma vždycky říkala, nábojů není nikdy dost!", Pic="army_shop.png" },
            new Location { Title = "Večerka", Description = "Velká pán, více platit!",Pic="vecerka.png" },
            new Location { Title = "Benzínka", Description = "Není lepší párek v rohlíku, než z benzínky." },
            new Location { Title = "Varna", Description = "Tady vyrábíš to nejčistší piko ve městě!"},
            new Location { Title = "Letiště", Description = "Tvoje cesta pryč z tohohle MORDORU!" },
            new Location { Title = "Mapa", Description = "Mapa tohohle prohnilýho města.", Pic ="mapa.JPG" },
            new Location { Title = "KFC", Description="Manažer prodejny: Zase pozdě a uplně vožralej, ale tentokrát už ti to nedaruju. Sbal si všechny svoje věci a vypadni.", Pic="kfc.png"},
            new Location { Title= "KFC",Description = "Manažer prodejny: Nepros a vypadni, vypadni!!!",Pic="kfc.png" },//DIAL1_1
            new Location { Title="KFC", Description = "Manažer prodejny: Co jsem řekl to platí, takových co přišli jen jednou a víckrát se neukázali mám dost.", Pic="kfc.png"}//DIAL_1_1_1

        };

        private readonly List<Connection> _map = new()
        {
            new Connection { From = Places.Byt, NextPlace = Places.Mapa, Description = "<p>JÍT OBKOUKNOUT MĚSTO</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.KFC, Description = "<p>JÍT DO TVÉ VYSNĚNÉ PRÁCE V KFC</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Fgn, Description = "<p>JÍT OČÍHNOUT FUGNERKU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Vecerka, Description = "<p>JÍT NAKOUPIT NĚJAKÝ TO ZBOŽÍ DO VEČERKY</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Benzinka, Description = "<p>PODÍVAT SE CO SE NA BENZIKU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.ArmyShop, Description = "<p> KOUKNOUT SE CO TEN STAREJ DĚDEK DNESKA PRODÁVÁ V ARMYSHOPU </p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Namesti, Description = "<p>POBHLÍDNOUT SE PO NÁMĚSTÍ</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Byt, Description = "<p>ZPĚT DO BYTU</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.Letiste, Description = "<p>CESTA ZA LEPŠÍM ŽIVOTEM</p>", SpecialPlace = "Letiste" },
            new Connection { From = Places.KFC, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Fgn, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Vecerka, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Benzinka, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.ArmyShop, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Namesti, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Letiste, NextPlace = Places.Mapa, Description = "<p>ZPĚT NA MAPU</p>" },
            new Connection { From = Places.Fgn, NextPlace = Places.Trafika, Description = "<p>JÍT SI KOUPIT NĚJAKÝ CÍGA</p>" },
            new Connection { From = Places.Trafika, NextPlace = Places.Fgn, Description = "<p>ZPĚT NA FUGNERKU</p>" },
            new Connection { From = Places.Byt, NextPlace = Places.Varna, Description = "<p>UVAŘIT NĚJAKOU SPECIALTKU</p>", SpecialPlace = "Varna" },
            new Connection { From= Places.Vecerka, NextPlace = Places.Vecerka, MoneyPlace = "Vecerka", Moneydesc ="<p>Okrást</p>", MoneyGet = 100},
            new Connection { From = Places.Varna, Moneydesc= "<p>UVAŘIT PERNÍK (PERNÍK +1)</p>" },
            new Connection { From = Places.Varna, NextPlace = Places.Byt, Description = "<p>Zpět do bytu</p>" },
            //---------------------
            //dialog sekvence kfc
            new Connection { From = Places.KFC, NextPlace = Places.KFC_DIAL_1, Description="<p>Dobrej šéfe</p>"},
            new Connection { From = Places.KFC_DIAL_1, NextPlace = Places.Mapa, Description = "<p>Udav se ty špíno</p>" },
            new Connection { From = Places.KFC_DIAL_1, NextPlace = Places.KFC_DIAL_1_1, Description = "<p>Slibuji že už se to nestane</p>" },
            new Connection { From = Places.KFC_DIAL_1_1, NextPlace = Places.Mapa, Description = "<p>Běž se bodnout</p>" },
            new Connection { From = Places.KFC_DIAL_1_1, NextPlace = Places.KFC_DIAL_1_1_1, Description = "<p>Prosíím slibuji že už se to víckrát nestane.</p>" },
            new Connection { From = Places.KFC_DIAL_1_1_1, NextPlace = Places.Mapa, Description = "<p>Běž někam</p>" }



        };
        public bool ExistLocation(Places id)
        {
            return (_locations.Count > (int)id);
        }

        public List<Connection> GetConnectionsFrom(Places id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.From == id).ToList();
            }
            throw new InvalidLocationException();
        }

        public List<Connection> GetConnectionsTo(Places id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.NextPlace == id).ToList();
            }
            throw new InvalidLocationException();
        }

        public Location GetLocation(Places id)
        {
            if (ExistLocation(id))
            {
                return _locations[(int)id];
            }
            throw new InvalidLocationException();
        }
    }
}
