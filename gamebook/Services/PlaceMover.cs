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
            new Location { Title="Byt", Description="Nacházíš se ve svém bytě, který už jsi neuklízel roky."},
            new Location { Title = "KFC", Description = "Tvůj džob...pokaždé co sem přijdeš, cítíš ten oder přepáleného tuku." },
            new Location { Title = "FGN", Description = "Místo plné cigošů a bezďáků." },
            new Location { Title = "Trafika", Description = "Stará bába prodává nejlevnější cíga ve městě." },
            new Location { Title = "LIDL", Description = "Pokladna 4 VOLNÁÁÁ" },
            new Location { Title = "Náměstí", Description = "" },
            new Location { Title = "Army Shop", Description = "" },
            new Location { Title = "Večerka", Description = "Dobý den...dal by si pán čerstvá jeblka nebo pomrdanče." },
            new Location { Title = "Benzínka", Description = "Není lepší párek v rohlíku, než z benzínky." },
            new Location { Title = "Varna", Description = "" },
            new Location { Title = "Letiště", Description = "" },
            new Location {Title ="Mapa", Description=""}
        };

        private readonly List<Connection> _map = new()
        {
            new Connection { From = Places.Byt, NextPlace = Places.Mapa, Description = "<p>jdi obkouknout kam můžeš cestovat</p>" },
            new Connection { From = Places.Mapa, NextPlace = Places.KFC, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.Fgn, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.Vecerka, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.Benzinka, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.ArmyShop, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.Namesti, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.Byt, Description = "" },
            new Connection { From = Places.Mapa, NextPlace = Places.Letiste, Description = "" },
            new Connection { From = Places.Fgn, NextPlace = Places.Trafika, Description = "" },
            new Connection { From = Places.Byt, NextPlace = Places.Varna, Description = "" },


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

        public bool IsNavigationLegitimate(Places from, Places to, GameState state)
        {
            throw new NotImplementedException();
        }
    }
}
