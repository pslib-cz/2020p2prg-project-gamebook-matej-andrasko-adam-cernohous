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
    public class BazenModel : PageModel
    {
        private const string KEY = "game";
        private const string KEY2 = "character";
        private const string KEY3 = "money";
        private const string KEY4 = "list";
        private const string KEY5 = "hp";
        private const string KEYCHECK = "CHECK";
        private readonly ISessionStorage<GameState> _ss;
        private readonly ISessionStorage<GameState> _dd;
        private readonly IPlaceMover _pm;
        private Random _rn = new Random();

        public Location Location { get; set; }
        public Characters Character { get; set; }
        public List<Connection> Connections { get; set; }
        [TempData]
        public bool Open { get; set; } = true;
        public List<Item> itemy { get; set; }
        public int HP { get; set; }
        public GameState State { get; set; }
        public GameState Chload { get; set; }
        public int Money { get; set; }
        [TempData]
        public int Locked { get; set; }
        public bool Mess { get; set; }
        public bool MessTrue { get; set; }

        public BazenModel(ISessionStorage<GameState> ss, IPlaceMover pm, ISessionStorage<GameState> dd)
        {
            _ss = ss;
            _pm = pm;
            _dd = dd;
        }

        public ActionResult OnGet(Places id)
        {
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);

            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            _dd.Save(KEY2, Chload);

            Chload = _dd.LoadOrCreate(KEY3);
            Money = Chload.Money;
            _dd.Save(KEY3, Chload);

            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            _ss.Save(KEY4, State);

            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;
            _ss.Save(KEY5, State);
            State = _ss.LoadOrCreate(KEYCHECK);
            State.Current = id;
            _ss.Save(KEYCHECK, State);

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
            if (_pm.IsNavigationLegitimate(State.Check, State.Current, State) == false)
            {
                return RedirectToPage("GameOver");
            }


            State = _ss.LoadOrCreate(KEYCHECK);
            State.Check = id;
            _ss.Save(KEYCHECK, State);

            return Page();

        }
        public void OnGetSteal(Places id)
        {
            Locked = _rn.Next(0, 10);
            TempData.Keep();
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);

            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            _dd.Save(KEY2, Chload);

            if (Locked < 2)
            {
                Chload = _dd.LoadOrCreate(KEY3);
                Money = Chload.Money;
                Money = Money + _rn.Next(20, 250);
                Chload.Money = Money;
                _dd.Save(KEY3, Chload);
                MessTrue = true;
            }
            else
            {
                Chload = _dd.LoadOrCreate(KEY3);
                Money = Chload.Money;
                Chload.Money = Money;
                _dd.Save(KEY3, Chload);
                Mess = true;
            }

            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            _ss.Save(KEY4, State);

            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;
            _ss.Save(KEY5, State);


            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);

        }
    }
}
