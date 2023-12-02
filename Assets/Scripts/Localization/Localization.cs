using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Balda
{
    public class Localization : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private string _alias;

        [SerializeField]
        private bool _upperCase;

        public void UpdateText()
        {
            _text.text = LocalizationManager.Instance.Get(_alias);

            ToUpperCase();
        }

        public void SetAlias(string alias, params object[] args)
        {
            _alias = alias;
            _text.text = string.Format(LocalizationManager.Instance.Get(_alias), args);

            ToUpperCase();
        }

        private void ToUpperCase()
        {
            if (_upperCase)
            {
                _text.text = _text.text.ToUpper();
            }
        }

        void Start()
        {

        }
    }
}
