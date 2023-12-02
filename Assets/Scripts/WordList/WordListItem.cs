using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Balda
{
    public class WordListItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        public void SetWord(string word)
        {
            _text.text = $"{word} ({word.Length})";
        }

        void Start()
        {

        }
    }
}
