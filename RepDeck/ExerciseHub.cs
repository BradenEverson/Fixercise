using Microsoft.AspNetCore.SignalR;
using RepDeck.Core;
using RepDeck.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepDeck
{
    public class ExerciseHub : Hub
    {
        private readonly IDeckData decks;
        public ExerciseHub(IDeckData decks)
        {
            this.decks = decks;
        }
        public async Task NextExercise(string guid)
        {
            Deck currentDeck = decks.getByGuid(guid);
            currentDeck.cardDeck.Remove(currentDeck.cardDeck[0]);
            decks.update(currentDeck);
            Card newCurrentCard = decks.getByGuid(guid).cardDeck[0];
            await Clients.Caller.SendAsync("newExercise",newCurrentCard.name,newCurrentCard.muscleGroup,newCurrentCard.description);
        }
    }
}
