using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class WordListModel : IWordListModel
    {
        public List<string> Words { get; }

        public WordListModel()
        {
            Words = new List<string>();
        }

        public void Clear()
        {
            Words.Clear();
        }

        public string GetTotalText()
        {
            return $"Итого: {Words.Sum(w => w.Length)}";
        }
    }
}
