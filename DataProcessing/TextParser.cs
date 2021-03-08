using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;

namespace DataProcessing
{
    public class TextParser
    {
        public List<Tweet> Tweets { get; set; } /// delete from parser when possible
        private static TextParser _instance;

        private TextParser()
        {

        }

        public void ParseFile(string path)
        {
            Parallel.ForEach(File.ReadLines(path), (line, _, lineNumber) =>
            {
                Console.WriteLine(line);
            });
        }
        public static Tweet TweetParse(string tweetLine)
        {
            Regex pattern = new Regex(@"(\t\u005F\t|\t+)");
            string[] splitedTweetLine = pattern.Split(tweetLine);
            Tweet tweet = new Tweet(splitedTweetLine[4], CoordinatesParser(splitedTweetLine[0].Trim('[', ']').Split(", ")), DateParser(splitedTweetLine[2]));
            return tweet;
        }
        public static DateTime DateParser(string line)
        {
            DateTime.TryParse(line, out DateTime tweetDate);
            return tweetDate;
        }
        public static Coordinate CoordinatesParser(string[] coordinates)
        {
            Double.TryParse(coordinates[0], out double longitude);
            Double.TryParse(coordinates[1], out double latitude);
            return new Coordinate(longitude, latitude);
        }
        public static TextParser GetInstance()
        {
            return _instance ??= new TextParser();
        }
    }
}
