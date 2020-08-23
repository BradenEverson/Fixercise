using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
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
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html.Result);
            this.cardDeck = scrapeDeck(htmlDoc);
            this.deckGuid = Guid.NewGuid().ToString().Split('-')[0];
        }
        private List<Card> scrapeDeck(HtmlDocument html)
        {
            List<Card> deck = new List<Card>();
            var nameList = html.GetElementbyId("hor-minimalist_3");
            var names = nameList.Descendants("td").Where(r => r.GetAttributeValue("style","").Contains("margin")).ToList();
            List<string> nameUrls = new List<string>();
            for(int i = 0; i < names.Count; i+=2){
                //Console.WriteLine(names[i].InnerHtml.Split('\'')[1]);
                nameUrls.Add(names[i].InnerHtml.Split('\'')[1]);
            }
            foreach(string url in nameUrls)
            {
                string tempUrl = "https://www.jefit.com/exercises/" + url;
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Fixercise");
                var htmlAsync = httpClient.GetStringAsync(tempUrl);
                HtmlDocument exerciseManager = new HtmlDocument();
                exerciseManager.LoadHtml(htmlAsync.Result);
                var detailCore = exerciseManager.DocumentNode.Descendants("div").Where(r => r.GetAttributeValue("class", "").Contains("mt-2 p-2")).ToList();
                Dictionary<string, string> dataBind = new Dictionary<string, string>();
                foreach(var detail in detailCore)
                {
                    foreach(string keyValPair in detail.InnerHtml.Split("<strong>"))
                    {
                        if(keyValPair.Split("</p>")[0].Split("</strong>").Length >= 2)
                        {
                            string key = keyValPair.Split("</p>")[0].Split("</strong>")[0].Split("<")[0].Replace(":", " ").Replace(" ", string.Empty);
                            string val = keyValPair.Split("</p>")[0].Split("</strong>")[1].Split("<")[0].Replace(":", " ").Replace(" ", string.Empty);
                            //Console.WriteLine(key + ":" + val);
                            dataBind.Add(key, val);

                        }
                    }
                }
                string instructions;
                var instructionsTemp = exerciseManager.DocumentNode.Descendants("div").Where(r => r.GetAttributeValue("class", "").Contains("col-sm-6 mt-2"));
                if(instructionsTemp.ToList()[instructionsTemp.Count() - 1].InnerText.Split(":").Count() >= 2)
                {
                //Console.WriteLine(instructionsTemp.ToList()[instructionsTemp.Count()-1].InnerText);
                String serializedInstructions = instructionsTemp.ToList()[instructionsTemp.Count()-1].InnerText.Split(":")[1];
                instructions = serializedInstructions;
                Card newCard = new Card(url.Split('/')[1].Replace('-', ' '),dataBind["MainMuscleGroup"],instructions,dataBind["Difficulty"],dataBind["Type"]);
                deck.Add(newCard);
                }
            }
            List<Card> shuffledDeck = new List<Card>();
            for(int i = 0; i < deck.Count()/2; i++)
            {
                Card newCard = deck[StaticRandom.Instance.Next(0, deck.Count() - 1)];
                shuffledDeck.Add(newCard);
                deck.Remove(newCard);
            }
            return shuffledDeck;
        }
    }
}
