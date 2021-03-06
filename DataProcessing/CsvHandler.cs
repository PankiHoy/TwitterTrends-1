using Core;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DataProcessing
{
    public class CsvHandler
    {
        private static CsvHandler _instance;
        public Dictionary<string, double> Sentiments { get; private set; }
        private CsvHandler()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using var reader = new StreamReader(@"..\DataProcessing\DataToProcess\sentiments.csv");
            using var csv = new CsvReader(reader, config);
            Sentiments = ToDictionary(csv.GetRecords<Sentiment>());
        }
        public static CsvHandler GetCsvInstance()
        {
            return _instance ??= new CsvHandler();
        }

        private Dictionary<string, double> ToDictionary(IEnumerable<Sentiment> sentimentsEnumerable)
        {
            Dictionary<string, double> sentiments = new Dictionary<string, double>();

            foreach (Sentiment sentiment in sentimentsEnumerable)
            {
                sentiments.Add(sentiment.Text, sentiment.SentimentVal);
            }

            return sentiments;
        }
    }
}
