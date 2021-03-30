using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing
{
    public class StateDefiner
    {
        private JsonHandler _jsonHandler;

        public StateDefiner()
        {
            _jsonHandler = new JsonHandler();
        }

        public void DefineStates(IEnumerable<Tweet> tweets)
        {
            foreach (var tweet in tweets)
            {
                if (tweet == null) continue;
                foreach (var state in States.GetStatesInstance().StatesCollection)
                {
                    if (state.IsInnerPoint(tweet.Location))
                    {
                        tweet.State = state.PostalCode;
                        break;
                    }
                }
            }
        }
        public void DefineState(Tweet tweet)
        {
            if (tweet == null) return;
            foreach (var state in States.GetStatesInstance().StatesCollection)
            {
                if (state.IsInnerPoint(tweet.Location))
                {
                    tweet.State = state.PostalCode;
                    state.Tweets.Add(tweet);    //ADDED
                    break;
                }
            }
        }
    }
}
