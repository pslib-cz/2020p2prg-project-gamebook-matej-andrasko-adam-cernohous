using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using gamebook.Models;
using gamebook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gamebook.Pages
{
    public class namestiBojModel : PageModel
    {
        private const string KEY = "game";
        private const string KEY2 = "character";
        private const string KEY3 = "money";
        private const string KEY4 = "list";
        private const string KEY5 = "hp";
        private const string KEY6 = "bosskilled";
        private const string KEY7 = "hpboss";
        private readonly ISessionStorage<GameState> _ss;
        private readonly ISessionStorage<GameState> _dd;
        private readonly IPlaceMover _pm;
        private Random _rn = new Random();

        public Location Location { get; set; }
        public Characters Character { get; set; }
        public List<Connection> Connections { get; set; }
        public List<Item> itemy { get; set; }
        public int HP { get; set; }
        public GameState State { get; set; }
        public GameState Chload { get; set; }
        public int Money { get; set; }
        public int number { get; set; }
        public string Sound { get; set; }
        public int bossHP { get; set; }
        public bool bossAlive { get; set; }
        public int bossHPbefore { get; set; }
        private const string KEYCHECK = "CHECK";

        public namestiBojModel(ISessionStorage<GameState> ss, IPlaceMover pm, ISessionStorage<GameState> dd)
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


            State = _ss.LoadOrCreate(KEY6);
            bossAlive = State.BossAlive;
            _ss.Save(KEY6, State);
            
            State = _ss.LoadOrCreate(KEY7);
            bossHP = State.bossHP;
            _ss.Save(KEY7, State);
            State = _ss.LoadOrCreate(KEYCHECK);
            State.Current = id;
            _ss.Save(KEYCHECK, State);


            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
            if (_pm.IsNavigationLegitimate(State.Check, State.Current, State) == false)
            {
                return RedirectToPage("CheaterScreen");
            }


            State = _ss.LoadOrCreate(KEYCHECK);
            State.Check = id;
            _ss.Save(KEYCHECK, State);

            if (Location.Sound is null)
            {

            }
            else
            {
                number = _rn.Next(0, Location.Sound.Count);
                Sound = Location.Sound[number];
            }
            return Page();
        }
        public ActionResult OnGetHit(Places id)
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
            HP -= _rn.Next(0, 2);
            State.HP = HP;
            _ss.Save(KEY5, State);
            if (HP <= 0)
            {
                return RedirectToPage("GameOver");
            }

            State = _ss.LoadOrCreate(KEY6);
            bossAlive = State.BossAlive;
            _ss.Save(KEY6, State);

            State = _ss.LoadOrCreate(KEY7);
            bossHP = State.bossHP;
            _ss.Save(KEY7, State);



            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
            return Page();
        }
        public void OnGetDamageGlock (Places id)
        {
            bossHPbefore = bossHP;
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

            State = _ss.LoadOrCreate(KEY6);
            bossAlive = State.BossAlive;
            _ss.Save(KEY6, State);

            State = _ss.LoadOrCreate(KEY7);
            bossHP = State.bossHP;
            bossHP -= _rn.Next(3, 5);
            State.bossHP = bossHP;
            _ss.Save(KEY7, State);

            if (bossHP <= 0)
            {
                State = _ss.LoadOrCreate(KEY6);
                bossAlive = State.BossAlive;
                bossAlive = false;
                State.BossAlive = bossAlive;
                _ss.Save(KEY6, State);

                Chload = _dd.LoadOrCreate(KEY3);
                Money = Chload.Money;
                Money = Money + 3000;
                Chload.Money = Money;
                _dd.Save(KEY3, Chload);
            }

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);
        }
        public void OnGetDamage(Places id)
        {
            bossHPbefore = bossHP;
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

            State = _ss.LoadOrCreate(KEY6);
            bossAlive = State.BossAlive;
            _ss.Save(KEY6, State);

            State = _ss.LoadOrCreate(KEY7);
            bossHP = State.bossHP;
            bossHP -= _rn.Next(1, 2);
            State.bossHP = bossHP;
            _ss.Save(KEY7, State);

            if (bossHP <= 0)
            {
                State = _ss.LoadOrCreate(KEY6);
                bossAlive = State.BossAlive;
                bossAlive = false;
                State.BossAlive = bossAlive;
                _ss.Save(KEY6, State);

                Chload = _dd.LoadOrCreate(KEY3);
                Money = Chload.Money;
                Money = Money + 3000;
                Chload.Money = Money;
                _dd.Save(KEY3, Chload);

            }

            Location = _pm.GetLocation(id);
            Connections = _pm.GetConnectionsFrom(id);

        }

    }
}
