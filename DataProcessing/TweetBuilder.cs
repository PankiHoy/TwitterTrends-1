using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace DataProcessing
{
    public class TweetBuilder
    {
        private SentimentCounter _sentimentCounter;
        //private StateDifiner _stateDefiner;
        private TextParser _txtParser;

        public TweetBuilder()
        {
            _sentimentCounter = new SentimentCounter();
            _txtParser = TextParser.GetInstance();
            //_stateDefiner = new StateDefiner();
        }
        public Tweet BuildTweet()
        {
            return null;
        }

    }
}
