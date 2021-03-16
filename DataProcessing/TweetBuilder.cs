using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace DataProcessing
{
    public class TweetBuilder
    {
        private SentimentCounter _sentimentCounter;
        //private StateDifiner _stateDefiner;
        private readonly TextParser _txtParser;
        private List<Tweet> _tweets;
        public List<Tweet> Tweets => _tweets;

        public TweetBuilder()
        {
            _tweets = new List<Tweet>();
            _sentimentCounter = new SentimentCounter();
            _txtParser = TextParser.GetInstance();

            //_stateDefiner = new StateDefiner();
        }
        public  void BuildTweet(string tweetLine)
        {
            Tweet tweet = new Tweet();
           _txtParser.TweetParse(tweetLine, tweet);
            _sentimentCounter.CountSentiments(tweet);
            
            //_stateDefiner.DefineState(tweet);
            _tweets.Add(tweet);
            
        }

        public async void BuildTweets(string path)
        {
            foreach (var line in _txtParser.GetFileLines(path))
            {
                await Task.Run(() => BuildTweet(line));
            }
        }


    }
}
