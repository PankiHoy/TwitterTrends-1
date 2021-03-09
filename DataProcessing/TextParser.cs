using Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace DataProcessing
{
    public class TextParser
    {
        public List<Tweet> Tweets { get; set; } /// delete from parser when possible
        private static TextParser _instance;

        private TextParser()
        {
            Tweets = new List<Tweet>();
        }

        public void ParseFile(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                Tweets.Add(TweetParse(line));
            }


        }
        public Tweet TweetParse(string tweetLine)
        {
            try
            {
                Regex pattern = new Regex(@"(\t\u005F\t|\t+)");
                string[] splitedTweetLine = pattern.Split(tweetLine);
                Tweet tweet = new Tweet(splitedTweetLine[4],
                    CoordinatesParser(splitedTweetLine[0].Trim('[', ']').Split(", ")), DateParser(splitedTweetLine[2]));
                return tweet;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }


        }
        public static DateTime DateParser(string line)
        {
            DateTime.TryParse(line, out DateTime tweetDate);
            return tweetDate;
        }
        public static Coordinate CoordinatesParser(string[] coordinates)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            double latitude = Double.Parse(coordinates[0], formatter);
            double longitude = Double.Parse(coordinates[1], formatter);
            return new Coordinate(longitude, latitude);
        }
        public static TextParser GetInstance()
        {
            return _instance ??= new TextParser();
        }
    }
}
