using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public static TextParser GetInstance()
        {
            return _instance ??= new TextParser();
        }
    }
}
