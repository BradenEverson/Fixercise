using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepDeck.Core;
using RepDeck.Data;
using RepDeck.Database;

namespace RepDeck
{
    public class CreateDeckModel : PageModel
    {
        public readonly IDeckData decks;
        public readonly RepDeckContext db;
        public User currentUser;
        public CreateDeckModel(IDeckData decks, RepDeckContext db)
        {
            this.decks = decks;
            this.db = db;
        }
        [BindProperty]
        public string muscleGroups { get; set; } = "";
        [BindProperty]
        public string Difficulty { get; set; } = "";
        [BindProperty]
        public string Type { get; set; } = "";
        public void OnGet()
        {
            currentUser = db.Users.FirstOrDefault(r => r.Email == User.Identity.Name);
        }
        public IActionResult OnPost()
        {
            Deck newDeck = new Deck("https://www.exercise.com/exercises?search=&" + muscleGroups + Difficulty + Type);
            decks.add(newDeck);
            return RedirectToPage("./Activity", new {DeckGuid = newDeck.deckGuid});
        }
    }
}