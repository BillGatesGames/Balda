using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Balda
{
    public class WordListItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _word;

        [SerializeField]
        private TextMeshProUGUI _length;

        public void SetWord(string word)
        {
            _word.text = word;
            _length.text = word.Length.ToString();
        }

        void Start()
        {

        }
    }
}
