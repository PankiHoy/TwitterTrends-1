using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace DataProcessing
{
   public class CsvHandler
   {
       private static CsvHandler _instance;
        public IEnumerable<Sentiment> Sentiments { get; private set; }
        private CsvHandler()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using var reader = new StreamReader("C:\\Users\\temoh\\source\\repos\\TwitterTrends\\DataProcessing\\sentiments.csv");
            using var csv = new CsvReader(reader, config);
            Sentiments = csv.GetRecords<Sentiment>().ToList();
        }

        public static CsvHandler GetCsvInstance()
        {
            return _instance ??= new CsvHandler();
        }
   }
}
