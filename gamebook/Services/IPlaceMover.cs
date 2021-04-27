using gamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Services
{
    public interface IPlaceMover
    {
        bool ExistLocation(Places id);
        Location GetLocation(Places id);
        List<Connection> GetConnectionsFrom(Places id);
        List<Connection> GetConnectionsTo(Places id);
    }
}