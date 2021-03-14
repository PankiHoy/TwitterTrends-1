using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.PhrasesMatching
{
    /// <summary>
    /// Trie that will find strings or phrases and return values of type <typeparamref name="T"/>
    /// for each string or phrase found.
    /// </summary>
    /// <remarks>
    /// <typeparamref name="T"/> will typically be a char for finding strings
    /// or a string for finding phrases or whole words.
    /// </remarks>
    /// <typeparam name="T">The type of a letter in a word.</typeparam>
    /// <typeparam name="TValue">The type of the value that will be returned when the word is found.</typeparam>
    public class Trie
    {
        private char[] separator = {' ', '.', ',', '#', '\t'};
        /// <summary>
        /// Root of the trie. It has no value and no parent.
        /// </summary>
        private readonly Node _root = new Node();

        /// <summary>
        /// Adds a word to the tree.
        /// </summary>
        /// <remarks>
        /// A word consists of letters. A node is built for each letter.
        /// If the letter type is char, then the word will be a string, since it consists of letters.
        /// But a letter could also be a string which means that a node will be added
        /// for each word and so the word is actually a phrase.
        /// </remarks>
        /// <param name="phrase">The word that will be searched.</param>
        /// <param name="value">The value that will be returned when the word is found.</param>
        public void Add(string phrase)
        {
            // start at the root
            var node = _root;

            IEnumerable<string> words = phrase.Split();//with separator?

            // build a branch for the word, one letter at a time
            // if a letter node doesn't exist, add it
            foreach (string word in words)
            {
                var child = node[word];

                if (child == null)
                    child = node[word] = new Node(word);

                node = child;
            }

            // mark the end of the branch
            // by adding a value that will be returned when this word is found in a text
            node.IsEndPhrase = true;
        }

        /// <summary>
        /// Finds all added words in a text.
        /// </summary>
        /// <param name="text">The text to search in.</param>
        /// <returns>The values that were added for the found words.</returns>
        public List<string> Find(string text)
        {
            var node = _root;

            List<string> foundPhrases = new List<string>();
            StringBuilder phrase = new StringBuilder();

            string[] words = text.Split(separator);//change to Regex.Split?
            for (int i = 0; i < words.Length;)
            {
                var wordToSearch = words[i].ToLower();
                if (node[wordToSearch] != null)
                {
                    node = node[wordToSearch];
                    phrase.Append(wordToSearch);
                    phrase.Append(' ');
                    ++i;
                }
                else
                {
                    if (node.IsEndPhrase)
                    {
                        phrase.Remove(phrase.Length - 1, 1);
                        foundPhrases.Add(phrase.ToString());
                    }

                    if (node == _root)
                        ++i;
                    else
                    {
                        node = _root;
                        phrase.Clear();
                    }
                }
            }

            if (node.IsEndPhrase)
            {
                phrase.Remove(phrase.Length - 1, 1);
                foundPhrases.Add(phrase.ToString());
            }
            return foundPhrases;
        }


        /// <summary>
        /// Node in a trie.
        /// </summary>
        /// <typeparam name="TNode">The same as the parent type.</typeparam>
        /// <typeparam name="TNodeValue">The same as the parent value type.</typeparam>
        private class Node : IEnumerable<Node>
        {
            private readonly string _word;
            private readonly Dictionary<string, Node> _children = new Dictionary<string, Node>();
            private bool _isEndPhrase = false;

            /// <summary>
            /// Constructor for the root node.
            /// </summary>
            public Node()
            {
            }

            /// <summary>
            /// Constructor for a node with a word
            /// </summary>
            /// <param name="word"></param>
            public Node(string word)
            {
                _word = word;
            }

            /// <summary>
            /// Word (or letter) for this node.
            /// </summary>
            public string Word
            {
                get { return _word; }
            }

            public bool IsEndPhrase
            {
                get => _isEndPhrase;
                set => _isEndPhrase = value;
            }

            /// <summary>
            /// Children for this node.
            /// </summary>
            /// <param name="c">Child word.</param>
            /// <returns>Child node.</returns>
            public Node this[string c]
            {
                get { return _children.ContainsKey(c) ? _children[c] : null; }
                set { _children[c] = value; }
            }


            /// <inherit/>
            public IEnumerator<Node> GetEnumerator()
            {
                return _children.Values.GetEnumerator();
            }

            /// <inherit/>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <inherit/>
            public override string ToString()
            {
                return Word.ToString();
            }
        }
    }
}
