using System;
using Core;
using System.Collections.Generic;
using System.Linq;
namespace DataProcessing
{
    public class TweetBuilder
    {
        private SentimentCounter _sentimentCounter;
        private StateDefiner _stateDefiner;
        private readonly TextParser _txtParser;
        private List<Tweet> _tweets;
        private States _states;
        public Dictionary<State, double> StatesToDisplay { get; }
        public List<State> StatesForDisplay { get; set; }
        public List<Tweet> Tweets => _tweets;

        public TweetBuilder()
        {

            _tweets = new List<Tweet>();
            _sentimentCounter = new SentimentCounter();
            _txtParser = TextParser.GetInstance();
            _states = States.GetStatesInstance();
            _stateDefiner = new StateDefiner();
            _states = States.GetStatesInstance();
            StatesForDisplay = new List<State>(_states.StatesCollection);
            StatesToDisplay = new Dictionary<State, double>();

        }
        public void BuildTweet(string tweetLine)
        {
            Tweet tweet = new Tweet();
            _txtParser.TweetParse(tweetLine, tweet);
            _sentimentCounter.CountSentimentsForTweet(tweet);
            _stateDefiner.DefineState(tweet);
            _tweets.Add(tweet);
        }

        public void BuildTweets(string path)
        {
            foreach (var line in _txtParser.GetFileLines(path))
            {
                BuildTweet(line);
            }

            var buffer = new List<State>();
            var query = Tweets.GroupBy(x => x.State);
            foreach (var group in query)
            {
                if (group.Key == null) continue;
                //StatesToDisplay.Add(StatesForDisplay.Find(x => x.PostalCode.Equals(group.Key)),//ONE STATE IS NULL(MAYBE MORE)
                //    group.Average(x => x.Sentiments));

                buffer.Add(StatesForDisplay.Find(x => x.PostalCode.Equals(group.Key)));//ONE STATE IS NULL(MAYBE MORE)
                buffer.Last<State>().Sentiment = group.Average(x => x.Sentiments) * 10000;
            }

            StatesForDisplay = buffer;
        }



    }
}
