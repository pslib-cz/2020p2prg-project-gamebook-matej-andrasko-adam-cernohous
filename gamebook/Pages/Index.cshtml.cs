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
        [BindProperty]
        public Characters Character { get; set; }
        public GameState State { get; set; }

        public void OnGet()
        {
        }
        public ActionResult OnPost()
        {
            return RedirectToPage("/Place", new { character = Character });
        }
    }
}
