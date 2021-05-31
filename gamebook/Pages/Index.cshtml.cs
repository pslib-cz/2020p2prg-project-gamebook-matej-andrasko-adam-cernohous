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
    public class IndexModel : PageModel
    {
        private const string KEY2 = "character";
        private const string KEY3 = "money";
        private const string KEY4 = "list";
        private const string KEY5 = "hp";

        private readonly ISessionStorage<GameState> _dd;
        private readonly ISessionStorage<GameState> _ss;
        public IndexModel(ISessionStorage<GameState> dd, ISessionStorage<GameState> ss)
        {
            _dd = dd;
            _ss = ss;
        }

        [BindProperty]
        public Characters Character { get; set; }
        public GameState Chload { get; set; }
        public List<Item> itemy { get; set; } = null;
        public GameState State { get; set; }
        public int HP { get; set; } = 5;
        public int Money { get; set; } = 0; //jen tak

        public void OnGet()
        {

            Chload = _dd.LoadOrCreate(KEY4);
            itemy = Chload.Items;
            _dd.Save(KEY4, Chload);

            State = _ss.LoadOrCreate(KEY5);
            State.HP = 5;
            _ss.Save(KEY5, State);

            Chload = _ss.LoadOrCreate(KEY3);
            Chload.Money = Money;
            _ss.Save(KEY3, Chload);
            

        }
        public ActionResult OnPost()
        {
            Chload = _dd.LoadOrCreate(KEY2);
            Chload.Character = Character;
            _dd.Save(KEY2, Chload);

            return RedirectToPage("/Place");
        }
    }
}
