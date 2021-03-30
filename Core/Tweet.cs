using System;
using System.Collections.Generic;

namespace Core
{
    public class Tweet
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Coordinate Location { get; set; }
        public double Sentiments { get; set; } = 0;
        public string State { get; set; }
        public Tweet(string tweetText, Coordinate coordinate, DateTime date)
        {
            Text = tweetText;
            Date = date;
            Location = coordinate;

        }
        public Tweet()
        {

        }
    }
}
