using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class WordListModel : IWordListModel
    {
        private ILocalizationManager _localizationManager;

        public List<string> Words { get; }

        public WordListModel(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
            Words = new List<string>();
        }

        public void Init()
        {
            Words.Clear();
        }

        public string GetTotalText()
        {
            return string.Format(_localizationManager.Get("total"), Words.Sum(w => w.Length));
        }
    }
}
