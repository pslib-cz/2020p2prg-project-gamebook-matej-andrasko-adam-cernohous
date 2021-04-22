using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gamebook.Models;
using gamebook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gamebook.Pages
{
    public class PlaceModel : PageModel
    {
        private const string KEY = "game";
        private readonly ISessionStorage<GameState> _ss;
        private readonly IPlaceMover _pm;

        public Location Location { get; set; }
        public Characters Character { get; set; }
        public List<Connection> Connections { get; set; }
        public GameState State { get; set; }

        public PlaceModel(ISessionStorage<GameState> ss, IPlaceMover pm)
        {
            _ss = ss;
            _pm = pm;
        }

        public void OnGet(Places id, Characters character)
        {
            Character = character;
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);
            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
            
        }
    }
}

