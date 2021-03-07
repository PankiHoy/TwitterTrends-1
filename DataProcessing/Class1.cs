using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing
{
    class SuffixTrieNode
    {
        static int MAX_CHAR = 256;

        public SuffixTrieNode[] children = new SuffixTrieNode[MAX_CHAR];
        public List<int> indexes;

        public SuffixTrieNode() // Constructor 
        {
            // Create an empty linked list for indexes of 
            // suffixes starting from this node 
            indexes = new List<int>();

            // Initialize all child pointers as NULL 
            for (int i = 0; i < MAX_CHAR; i++)
                children[i] = null;
        }

        // A recursive function to insert a suffix of  
        // the text in subtree rooted with this node 
        public void insertSuffix(String s, int index)
        {

            // Store index in linked list 
            indexes.Add(index);

            // If string has more characters 
            if (s.Length > 0)
            {

                // Find the first character 
                char cIndex = s[0];

                // If there is no edge for this character, 
                // add a new edge 
                if (children[cIndex] == null)
                    children[cIndex] = new SuffixTrieNode();

                // Recur for next suffix 
                children[cIndex].insertSuffix(s.Substring(1),
                                                  index + 1);
            }
        }

        // A function to search a pattern in subtree rooted 
        // with this node.The function returns pointer to a  
        // linked list containing all indexes where pattern  
        // is present. The returned indexes are indexes of  
        // last characters of matched text. 
        public List<int> search(String s)
        {

            // If all characters of pattern have been  
            // processed, 
            if (s.Length == 0)
                return indexes;

            // if there is an edge from the current node of 
            // suffix tree, follow the edge. 
            if (children[s[0]] != null)
                return (children[s[0]]).search(s.Substring(1));

            // If there is no edge, pattern doesnt exist in  
            // text 
            else
                return null;
        }
    }

    // A Trie of all suffixes 
    public class Suffix_tree
    {

        SuffixTrieNode root = new SuffixTrieNode();

        // Constructor (Builds a trie of suffies of the 
        // given text) 
        Suffix_tree(String txt)
        {

            // Consider all suffixes of given string and 
            // insert them into the Suffix Trie using  
            // recursive function insertSuffix() in  
            // SuffixTrieNode class 
            for (int i = 0; i < txt.Length; i++)
                root.insertSuffix(txt.Substring(i), i);
        }

        /* Prints all occurrences of pat in the  
        Suffix Trie S (built for text) */
        void search_tree(String pat)
        {
            // Let us call recursive search function  
            // for root of Trie. 
            // We get a list of all indexes (where pat is  
            // present in text) in variable 'result' 
            List<int> result = root.search(pat);

            // Check if the list of indexes is empty or not 
            if (result == null)
                Console.WriteLine("Pattern not found");
            else
            {
                int patLen = pat.Length;

                foreach (int i in result)
                    Console.WriteLine("Pattern found at position " +
                                                      (i - patLen));
            }
        }

        // Driver Code 
        public static void Main(String[] args)
        {

            //var tweets = new string[]
            //{
            //    "My Cali girl Katie at a tractor pull might be the funniest thing ever  @pix_perfect12",
            //    "I'm so excited to be back in Cali a week from today. : ) I will miss many things about living in WA though.",
            //    "order plane tickets, cali is the mission."
            //};

            //String txt = tweets[1];
            //Suffix_tree S = new Suffix_tree(txt);
            //Console.WriteLine("Search for in");
            //S.search_tree("in");

            // Let us build a suffix trie for text  
            // "geeksforgeeks.org" 
            //String txt = "geeksforgeeks.org";
            //Suffix_tree S = new Suffix_tree(txt);

            //Console.WriteLine("Search for 'ee'");
            //S.search_tree("ee");

            //Console.WriteLine("\nSearch for 'geek'");
            //S.search_tree("geek");

            //Console.WriteLine("\nSearch for 'quiz'");
            //S.search_tree("quiz");

            //Console.WriteLine("\nSearch for 'forgeeks'");
            //S.search_tree("forgeeks");


            //DateTime start = DateTime.Now;
            
            
            //DateTime end = DateTime.Now;

            //TimeSpan elapsed = end - start;
            //Console.WriteLine(elapsed.TotalSeconds);

            var parser = TextParser.GetInstance();
            parser.ParseFile(
                "C:\\Users\\temoh\\source\\repos\\TwitterTrends\\DataProcessing\\DataToProcess\\cali_tweets2014.txt");




        }
    }
}
