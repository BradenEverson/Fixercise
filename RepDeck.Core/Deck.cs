using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RepDeck.Core
{
    public class Deck
    {
        public List<Card> cardDeck{ get; set; }
        public string deckGuid { get; }
        public Deck(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Fixercise");
            var html = httpClient.GetStringAsync(url);
            var htmlDoc = new HtmlDocument();
            this.cardDeck = scrapeDeck(htmlDoc);
            this.deckGuid = Guid.NewGuid().ToString().Split('-')[0];
        }
        private List<Card> scrapeDeck(HtmlDocument htmlDoc)
        {
            List<Card> deck = new List<Card>();
            
            return deck;
        }
    }
}
