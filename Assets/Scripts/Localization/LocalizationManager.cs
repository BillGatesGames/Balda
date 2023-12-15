using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        private LocalizationData _data;
        private HashSet<string> _langs;
        private string _lang = string.Empty;

        public string GetAlphabet()
        {
            CheckLang();

            return _data.Configs[_lang].Alphabet;
        }

        public string GetDictionaryFileName()
        {
            CheckLang();

            return _data.Configs[_lang].FileName;
        }

        public void SetLang(string lang)
        {
            lang = lang.ToUpper();

            _lang = _langs.Contains(lang) ? lang : Constants.Localization.FALLBACK_LANG;
        }

        public string Get(string alias)
        {
            CheckLang();

            if (_data != null)
            {
                alias = alias.ToLower();

                if (_data.Localization.ContainsKey(alias))
                {
                    return _data.Localization[alias][_lang];
                }
            }

            return Constants.Localization.ERROR_PREFIX + alias;
        }

        public void LoadLocalization()
        {
            _data = ResourceLoader.Instance.LoadLocal<LocalizationData>(Constants.Localization.FILE_NAME);
            _langs = new HashSet<string>();
            
            foreach (var lang in _data.Configs.Keys)
            {
                if (!_data.Localization.ContainsKey(lang))
                {
                    _langs.Add(lang);
                }
            }
        }

        public void UpdateLocalization()
        {
            var list = FindObjectsOfType<Localization>(true).ToList();

            foreach (var item in list)
            {
                item.UpdateText();
            }
        }

        private void CheckLang()
        {
            if (string.IsNullOrEmpty(_lang))
            {
                SetLang(Constants.Localization.FALLBACK_LANG);
            }
        }
    }
}
