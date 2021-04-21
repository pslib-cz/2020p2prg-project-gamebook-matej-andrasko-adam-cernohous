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
        };

        private readonly List<Connection> _map = new()
        {

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
