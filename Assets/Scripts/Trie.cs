using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Balda
{
    public class Node
    {
        public readonly char Letter;
        public bool IsTerminal { get; set; }
        public Dictionary<char, Node> Children { get; private set; }

        public Node(char letter, bool isTerminal)
        {
            Letter = letter;
            IsTerminal = isTerminal;
            Children = new Dictionary<char, Node>();
        }
    }

    public class Trie
    {
        public Node Root { get; private set; }

        private HashSet<string> _words;
        public IReadOnlyCollection<string> Words
        {
            get
            {
                return _words;
            }
        }

        private HashSet<string> GetWordsList()
        {
            var asset = Resources.Load(LocalizationManager.Instance.GetDictionaryFileName()) as TextAsset;
            var words = asset.text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            return new HashSet<string>(words);
        }

        private Node Build()
        {
            _words = GetWordsList();

            var root = new Node('#', false);

            foreach (var word in _words)
            {
                var curr = root;

                for (int i = 0; i < word.Length; i++)
                {
                    bool isTerminal = i == word.Length - 1;

                    if (curr.Children.ContainsKey(word[i]))
                    {
                        curr = curr.Children[word[i]];

                        if (isTerminal)
                        {
                            curr.IsTerminal = isTerminal;
                        }
                    }
                    else
                    {
                        Node node = new Node(word[i], isTerminal);
                        curr.Children.Add(word[i], node);
                        curr = node;
                    }
                }
            }

            return root;
        }

        public Trie()
        {
            Root = Build();
        }
    }
}
