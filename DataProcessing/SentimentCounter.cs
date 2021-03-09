﻿using Core;
using DataProcessing.PhrasesMatching;
using System.Collections.Generic;

namespace DataProcessing
{
    public class SentimentCounter
    {
        private Trie _trie;
        private CsvHandler _csvHandler;
        public SentimentCounter()
        {
            _trie = new Trie();
            _csvHandler = CsvHandler.GetCsvInstance();
            foreach (var key in _csvHandler.Sentiments.Keys)
            {
                _trie.Add(key);
            }

            _trie.Build();
        }

        public void CountSentiments(IEnumerable<Tweet> tweets)
        {
            foreach (var tweet in tweets)
            {
                if (tweet == null) continue;
                foreach (var match in _trie.Find(tweet.Text))
                {
                    if (_csvHandler.Sentiments.TryGetValue(match, out var value))
                    {
                        tweet.Sentiments += value;
                    }
                }
            }
        }
    }

}