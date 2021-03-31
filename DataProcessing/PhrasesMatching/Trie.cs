using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataProcessing.PhrasesMatching
{

    public class Trie
    {

        private Regex _regex;
        public Trie()
        {
            _regex = new Regex("[().,;:?\\x22/!@#\\s%|\"]");
        }

        private readonly Node _root = new Node();

        public void Add(string phrase)
        {
            var node = _root;

            IEnumerable<string> words = phrase.Split();
            foreach (string word in words)
            {
                var child = node[word];

                if (child == null)
                    child = node[word] = new Node(word, node);

                node = child;
            }

            node.IsEndPhrase = true;
        }

        public List<string> Find(string text)
        {
            if (text == null) return null;//ADD EXCEPTION
            var node = _root;

            List<string> foundPhrases = new List<string>();
            StringBuilder phrase = new StringBuilder();

            var nextWord = 0;
            string[] words = _regex.Split(text);
            for (int i = 0; i < words.Length;)
            {
                var wordToSearch = words[i].ToLower();
                if (node[wordToSearch] != null)
                {
                    if (node[wordToSearch].Parent == _root)
                        nextWord = i + 1;
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
                        i = nextWord;
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

        private class Node : IEnumerable<Node>
        {
            private readonly string _word;
            private readonly Node _parent;
            private readonly Dictionary<string, Node> _children = new Dictionary<string, Node>();
            private bool _isEndPhrase = false;

            public Node()
            {
            }

            public Node(string word, Node parent)
            {
                _word = word;
                _parent = parent;
            }

            public Node Parent
            {
                get { return _parent; }
            }

            public string Word
            {
                get { return _word; }
            }

            public bool IsEndPhrase
            {
                get => _isEndPhrase;
                set => _isEndPhrase = value;
            }

            public Node this[string c]
            {
                get { return _children.ContainsKey(c) ? _children[c] : null; }
                set { _children[c] = value; }
            }


            public IEnumerator<Node> GetEnumerator()
            {
                return _children.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return Word.ToString();
            }
        }
    }
}
