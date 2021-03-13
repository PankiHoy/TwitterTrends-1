using Core;
using System;

namespace DataProcessing
{
    public class Class1
    {
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



            //Console.WriteLine(parser.Tweets.Count);


            //var jsonParser = new JsonHandler();

            //DateTime start = DateTime.Now;
            //State state = null;
            //foreach (State s in States.GetStatesInstance().StatesCollection)
            //{
            //    if (s.IsInnerPoint(parser.Tweets[228].Location))
            //        state = s;
            //}
            //DateTime end = DateTime.Now;
            //var elapsed = end - start;
            //Console.WriteLine(elapsed.TotalSeconds);

            //if (state != null)
            //    Console.Write(state.PostalCode);

            //Console.WriteLine();
            //Console.Write(parser.Tweets[228].Location.Latitude);
            //Console.Write(' ');
            //Console.Write(parser.Tweets[228].Location.Longitude);

            SentimentCounter sentimentCounter = new SentimentCounter();
            sentimentCounter.CountSentiments(TextParser.GetInstance().Tweets);

            Console.WriteLine(TextParser.GetInstance().Tweets[456].Text);
            Console.WriteLine(TextParser.GetInstance().Tweets[456].Sentiments);
            //added comment
        }
    }
}
