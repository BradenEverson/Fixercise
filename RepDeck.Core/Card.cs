using System;
using System.Collections.Generic;
using System.Text;

namespace RepDeck.Core
{
    public class Card
    {
        public int id { get; set; }
        public string name { get; }
        public string muscleGroup { get; }
        public string description { get; }
        public string difficulty { get; }
        public string type { get; }
        public Card(string name, string muscleGroup, string description, string difficulty, string type)
        {
            this.name = name;
            this.muscleGroup = muscleGroup;
            this.description = description;
            this.difficulty = difficulty;
            this.type = type;
        }
    }
}
