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
        [TempData]
        public bool Open { get; set; } = true;
        public List<Item> itemy { get; set; }
        public int HP { get; set; }
        public GameState State { get; set; }
        public GameState Chload { get; set; }
        public int Money { get; set; }
        public int number { get; set; }
        public string Sound { get; set; }

        public PlaceModel(ISessionStorage<GameState> ss, IPlaceMover pm, ISessionStorage<GameState> dd)
        {
            _ss = ss;
            _pm = pm;
            _dd = dd;
        }

        public ActionResult OnGet(Places id, int m)
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
            if(HP <= 0)
            {
                return RedirectToPage("GameOver");
            }
            return Page();

        }
        public void OnGetMoneyGet(Places id, int m)
        {
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);
            
            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            _dd.Save(KEY2, Chload);
            
            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            _ss.Save(KEY4, State);

            Chload = _dd.LoadOrCreate(KEY3);
            Money = Chload.Money;
            if (itemy.Contains(Item.Glock))
            {
                Money = Money + m*10;
            }
            else
            {
                Money = Money + m;
            }

            Chload.Money = Money;
            _dd.Save(KEY3, Chload);
            Open = false;
            TempData.Keep();



            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;
            _ss.Save(KEY5, State);

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);


        }
        public void OnGetBuy(Places id, int m, int h)
        {
            State = _ss.LoadOrCreate(KEY);
            State.Location = id;
            _ss.Save(KEY, State);

            Chload = _dd.LoadOrCreate(KEY2);
            Character = Chload.Character;
            _dd.Save(KEY2, Chload);

            Chload = _dd.LoadOrCreate(KEY3);
            Money = Chload.Money;

            State = _ss.LoadOrCreate(KEY5);
            HP = State.HP;

            if (Money < m)
            {
                
            }
            else
            {
                if (HP != 5)
                {
                    Money -= m;
                    Chload.Money = Money;
                    _dd.Save(KEY3, Chload);

                    HP += h;
                    State.HP = HP;
                    _ss.Save(KEY5, State);
                }
                else
                {
                    Chload.Money = Money;
                    _dd.Save(KEY3, Chload);
                    
                    State.HP = HP;
                    _ss.Save(KEY5, State);

                }

            }

            State = _ss.LoadOrCreate(KEY4);
            itemy = State.Items;
            _ss.Save(KEY4, State);






            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
        }
    }
}

