using gamebook.Models;
using gamebook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Pages
{
    public class PrivacyModel : PageModel
    {
        private const string KEY = "game";
        private const string KEY2 = "character";
        private const string KEY3 = "money";
        private const string KEY4 = "list";
        private const string KEY5 = "hp";
        private readonly ISessionStorage<GameState> _ss;
        private readonly ISessionStorage<GameState> _dd;
        private readonly IPlaceMover _pm;

        public PrivacyModel(ISessionStorage<GameState> ss, ISessionStorage<GameState> dd, IPlaceMover pm)
        {
            _ss = ss;
            _dd = dd;
            _pm = pm;
        }

        public Location Location { get; set; }
        public Characters Character { get; set; }
        public List<Connection> Connections { get; set; }
        public List<Item> itemy { get; set; }
        public GameState State { get; set; }
        public GameState Chload { get; set; }
        public int Money { get; set; }
        public int HP { get; set; }

        public ActionResult OnGet(Places id)
        {
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);

            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            Character = 0;
            Chload.Character = Character;
            _dd.Save(KEY2, Chload);

            Chload = _dd.LoadOrCreate(KEY3);
            Money = Chload.Money;
            if (Money < 20000)
            {
                return RedirectToPage("CheaterScreen");
            }
            Money = 0;
            Chload.Money = Money;
            _dd.Save(KEY3, Chload);

            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            itemy = null;
            State.Items = itemy;
            _ss.Save(KEY4, State);

            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;
            HP = 0;
            State.HP = HP;
            _ss.Save(KEY5, State);



            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);

            return Page();
        }
    }
}