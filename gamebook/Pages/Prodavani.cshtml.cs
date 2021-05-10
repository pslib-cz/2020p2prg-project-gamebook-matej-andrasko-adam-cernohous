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
    public class ProdavaniModel : PageModel
    {
        private const string KEY = "game";
        private const string KEY2 = "character";
        private const string KEY3 = "money";
        private const string KEY4 = "list";
        private const string KEY5 = "hp";
        private readonly ISessionStorage<GameState> _ss;
        private readonly ISessionStorage<GameState> _dd;
        private readonly IPlaceMover _pm;
        private Random _rn = new Random();

        public Location Location { get; set; }
        public Characters Character { get; set; }
        public List<Connection> Connections { get; set; }
        public List<Item> itemy { get; set; }
        [TempData]
        public bool Open { get; set; } = true;
        public int Police { get; set;}
        public GameState State { get; set; }
        public GameState Chload { get; set; }
        public int Money { get; set; }
        public int HP { get; set; }
        public int number { get; set; }
        public string Sound { get; set; }

        public ProdavaniModel(ISessionStorage<GameState> ss, IPlaceMover pm, ISessionStorage<GameState> dd)
        {
            _ss = ss;
            _pm = pm;
            _dd = dd;
        }

        public void OnGet(Places id)
        {
            Police = _rn.Next(0, 10);
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

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
            if (Location.Sound is null)
            {

            }
            else
            {
                number = _rn.Next(0, Location.Sound.Count);
                Sound = Location.Sound[number];
            }


        }
        public void OnGetVezeni (Places id)
        {
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);

            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            _dd.Save(KEY2, Chload);
            
            Chload = _dd.LoadOrCreate(KEY3);
            Money = 0;
            Chload.Money = Money;
            _dd.Save(KEY3, Chload);

            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            itemy.Clear();
            State.Items = itemy;
            _ss.Save(KEY4, State);
            
            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;
            HP -= 2;
            State.HP = HP;
            _ss.Save(KEY5, State);

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
        }
        public void OnGetProdat(Places id, int m)
        {
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);

            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            _dd.Save(KEY2, Chload);

            Chload = _dd.LoadOrCreate(KEY3);
            Money = Chload.Money;
            Money = Money + m;
            Chload.Money = Chload.Money + m;
            _dd.Save(KEY3, Chload);

            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            itemy.Remove(Item.Pernik);
            State.Items = itemy;
            _ss.Save(KEY4, State);

            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;
            _ss.Save(KEY5, State);

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
        }
    }
}
