using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class Letter : ICloneable
    {
        public char Char;
        public bool IsNew;
        public Vector2Int Pos;
        public Letter(char @char, Vector2Int pos, bool isNew = false)
        {
            Char = @char;
            Pos = pos;
            IsNew = isNew;
        }

        public object Clone()
        {
            return new Letter(Char, Pos, IsNew);
        }
    }

    public class Word : ICloneable
    {
        private string _word;
        public List<Letter> Letters;

        public Word()
        {
            Letters = new List<Letter>();
        }

        public Word(string word)
        {
            Letters = new List<Letter>();

            for (int i = 0; i < word.Length; i++)
            {
                Letters.Add(new Letter(word[i], Vector2Int.zero));
            }

            Cache();
        }

        public void Cache()
        {
            _word = ToString();
        }

        public object Clone()
        {
            var clone = new Word();

            foreach (var letter in Letters)
            {
                clone.Letters.Add((Letter)letter.Clone());
            }

            return clone;
        }

        public override string ToString()
        {
            string result = string.IsNullOrEmpty(_word) ? new string(Letters.Select(l => l.Char).ToArray()) : _word;
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is not Word)
            {
                return false;
            }

            return ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
