using Core;
using System;
using System.Diagnostics;

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

            //var parser = TextParser.GetInstance();
            //parser.ParseFile(
            //    @"..\..\..\DataToProcess\snow_tweets2014.txt");

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

            //SentimentCounter sentimentCounter = new SentimentCounter();
            //StateDefiner stateDefiner = new StateDefiner();
            //var txtParser = TextParser.GetInstance();
            //sentimentCounter.CountSentiments(txtParser.Tweets);
            //stateDefiner.DefineStates(txtParser.Tweets);


            //Console.WriteLine(txtParser.Tweets[0].Text);
            //Console.WriteLine(txtParser.Tweets[0].Sentiments);
            //foreach (var sent in txtParser.Tweets[0].SentVal)
            //{
            //    Console.WriteLine(sent);
            //}
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TweetBuilder tweetBuilder = new TweetBuilder();
           
            tweetBuilder.BuildTweets(@"..\..\..\DataToProcess\snow_tweets2014.txt");
            
            stopwatch.Stop();
            var a = tweetBuilder.StatesForDisplay;
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds);

        }
    }
}
