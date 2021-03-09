using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using DataProcessing.PhrasesMatching;
using System.Diagnostics;

namespace DataProcessing
{
  public class Class1 { 
        // Driver Code 
        public static void Main(String[] args)
        {
            //DateTime start = DateTime.Now;
            ////var jsonParser = JsonHandler.GetJsonInstance();
            //var csvParser = CsvHandler.GetCsvInstance();
            //DateTime end = DateTime.Now;

            //TimeSpan elapsed = end - start;
            //Console.WriteLine(elapsed.TotalSeconds);

            //foreach(var element in csvParser.Sentiments)
            //{
            //    Console.WriteLine(element.Key + " " +  element.Value);
            //}

            //DateTime start1 = DateTime.Now;
            //double value = 0;
            //csvParser.Sentiments.TryGetValue("nice", out value);
            //DateTime end1 = DateTime.Now;
            //TimeSpan elapsed1 = end1 - start1;
            //Console.WriteLine(elapsed1.TotalSeconds);
            //Console.WriteLine(value);


            //States states = States.GetStatesInstance();

            var parser = TextParser.GetInstance();
            parser.ParseFile(
                @"..\..\..\DataToProcess\cali_tweets2014.txt");

            //foreach (var tweet in parser.Tweets)
            //{
            //    Console.WriteLine(tweet.Date + " " + tweet.Text + " " + tweet.Location.Longitude + " " + tweet.Location.Latitude);
            //}


            DateTime start = DateTime.Now;

            
                 Trie trie = new Trie();
            CsvHandler csv = CsvHandler.GetCsvInstance();
            foreach (var phrase in csv.Sentiments.Keys)
            {
                trie.Add(phrase);
            }

            trie.Build();

            foreach (var tweet in parser.Tweets)
            {
                if (tweet != null)
                {
                    foreach (var match in trie.Find(tweet.Text))
                    {
                        double value;
                        if (csv.Sentiments.TryGetValue(match, out value))
                            tweet.Sentiments += value;
                    }
                }
                else continue;
            }

            DateTime end = DateTime.Now;
            Console.WriteLine(parser.Tweets.Count);
            var elapsed = end - start;
            Console.WriteLine(elapsed.TotalSeconds);



        }
    }
}
