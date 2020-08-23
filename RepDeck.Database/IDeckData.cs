using RepDeck.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepDeck.Database
{
    public interface IDeckData
    {
        public Deck add(Deck deck);
        public Deck delete(Deck deck);
        public Deck update(Deck updatedDeck);
        public Deck getByGuid(string id);
        public void addUserIdToDict(string id);
        public int increaseScore(string id);
        public int getScore(string id);
    }
}
