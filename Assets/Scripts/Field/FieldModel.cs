using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class FieldModel : IFieldModel
    {
        private int _size;
        private char?[,] _field;
        private Trie _trie;
        private HashSet<string> _excludedWords;

        public bool IsLocked { get; set; } = false;

        public IReadOnlyCollection<string> ExcludedWords
        {
            get
            {
                return _excludedWords;
            }
        }

        public Selection Selection
        {
            get;
            private set;
        }

        private Vector2Int? _lastCharPos;

        public Vector2Int? LastCharPos
        {
            get
            {
                return _lastCharPos;
            }
        }

        public bool IsFilled
        {
            get
            {
                for (int y = 0; y < _size; y++)
                {
                    for (int x = 0; x < _size; x++)
                    {
                        if (_field[x, y] == null)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public FieldModel(int size)
        {
            _size = size;

            Init();
        }

        public void Init()
        {
            int size = GetSize();

            if (size % 2 == 0)
            {
                throw new System.ArgumentException("Field size must be an odd number");
            }

            _size = size;
            _field = new char?[_size, _size];
            _trie = new Trie();
            _excludedWords = new HashSet<string>();

            Selection = new Selection(this);

            var words = _trie.Words.Where(w => w.Length == _size).ToList();

            if (words.Count == 0)
            {
                throw new System.Exception("No word found to initialize the field");
            }

            string word = words[UnityEngine.Random.Range(0, words.Count)];

            for (int x = 0; x < size; x++)
            {
                _field[x, size / 2] = word[x];
            }

            _excludedWords.Add(word);
        }

        public int GetSize()
        {
            return _size;
        }

        public Trie GetTrie()
        {
            return _trie;
        }

        public char?[,] GetField()
        {
            return _field;
        }

        public bool TrySetSelectedWord()
        {
            if (!_lastCharPos.HasValue)
            {
                return false;
            }

            if (!Selection.Positions.Contains(_lastCharPos.Value))
            {
                return false;
            }

            string word = Selection.GetWord();

            if (!_trie.Words.Contains(word))
            {
                return false;
            }

            _excludedWords.Add(word);
            _lastCharPos = null;

            return true;
        }

        public bool IsEmpty(Vector2Int pos)
        {
            return !_field[pos.x, pos.y].HasValue;
        }

        public void DeleteLastChar()
        {
            if (_lastCharPos.HasValue)
            {
                _field[_lastCharPos.Value.x, _lastCharPos.Value.y] = null;
            }

            _lastCharPos = null;
        }

        public void SetChar(Vector2Int pos, char? @char)
        {
            _lastCharPos = pos;
            _field[pos.x, pos.y] = @char;
        }
    }
}
