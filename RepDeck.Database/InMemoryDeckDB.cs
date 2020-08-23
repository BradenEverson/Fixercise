using RepDeck.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepDeck.Database
{
    public class InMemoryDeckDb : IDeckData
    {
        private List<Deck> decks { get; set; }
        private Dictionary<string,int> userScoreDict { get; set; }
        public InMemoryDeckDb()
        {
            decks = new List<Deck>();
            userScoreDict = new Dictionary<string, int>();
        }
        public Deck add(Deck deck)
        {
            decks.Add(deck);
            return deck;
        }

        public void addUserIdToDict(string id)
        {
            Console.WriteLine(id);
            userScoreDict.Add(id, 0);
        }

        public Deck delete(Deck deck)
        {
            decks.Remove(deck);
            return deck;
        }

        public Deck getByGuid(string id)
        {
            Deck gottenDeck = decks.FirstOrDefault(r => r.deckGuid == id);
            return gottenDeck;
        }

        public int getScore(string id)
        {
            return userScoreDict[id];
        }

        public int increaseScore(string id)
        {
            int currentScore = userScoreDict[id];
            userScoreDict.Remove(id);
            userScoreDict.Add(id, currentScore++);
            return userScoreDict[id];
        }

        public Deck update(Deck updatedDeck)
        {
            Deck deck = decks.FirstOrDefault(r => r.deckGuid == updatedDeck.deckGuid);
            if(deck != null)
            {
                deck.cardDeck = updatedDeck.cardDeck;
            }
            return deck;
        }
    }
}
