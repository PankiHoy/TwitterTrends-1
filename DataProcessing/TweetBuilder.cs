using Core;
using System.Collections.Generic;

namespace DataProcessing
{
    public class TweetBuilder
    {
        private SentimentCounter _sentimentCounter;
        private StateDefiner _stateDefiner;
        private readonly TextParser _txtParser;
        private List<Tweet> _tweets;
        public List<Tweet> Tweets => _tweets;

        public TweetBuilder()
        {
            _tweets = new List<Tweet>();
            _sentimentCounter = new SentimentCounter();
            _txtParser = TextParser.GetInstance();

            _stateDefiner = new StateDefiner();

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
        }



    }
}
