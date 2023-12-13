using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class AlphabetModel : IAlphabetModel
    {
        [Inject]
        private ILocalizationManager _localizationManager;

        private List<char> _chars;

        public IReadOnlyList<char> Chars
        {
            get
            {
                return _chars;
            }
        }

        public bool IsLocked { get; set; } = false;

        public void Initialize()
        {
            _chars = _localizationManager.GetAlphabet().ToCharArray().ToList();
        }
    }
}
