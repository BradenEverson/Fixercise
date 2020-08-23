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
    public class AchievementsModel : PageModel
    {
        public readonly IDeckData decks;
        public readonly RepDeckContext db;
        public User currentUser;
        public Deck currentDeck;
        public AchievementsModel(IDeckData decks, RepDeckContext db)
        {
            this.decks = decks;
            this.db = db;
        }
        public void OnGet()
        {
            currentUser = db.Users.FirstOrDefault(r => r.Email == User.Identity.Name);
        }
    }
}