﻿using gamebook.Models;
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
        private readonly ISessionStorage<GameState> _dd;

        public IndexModel(ISessionStorage<GameState> dd)
        {
            _dd = dd;
        }

        [BindProperty]
        public Characters Character { get; set; }
        public GameState Chload { get; set; }
        public int Money { get; set; } = 0; //jen tak

        public void OnGet()
        {
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
