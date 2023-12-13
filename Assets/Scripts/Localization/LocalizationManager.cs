using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class LocalizationManager : ILocalizationManager
    {
        private IResourceLoader _resourceLoader;
        private LocalizationData _data;
        private HashSet<string> _langs;
        private string _lang = string.Empty;

        public LocalizationManager(string lang, IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            _lang = lang;
        }

        public void Initialize()
        {
            LoadLocalization();
            UpdateLocalization();
        }

        public string GetAlphabet()
        {
            if (string.IsNullOrEmpty(_lang))
            {
                _lang = Constants.Localization.FALLBACK_LANG;
            }

            return _data.Configs[_lang].Alphabet;
        }

        public string GetDictionaryFileName()
        {
            if (string.IsNullOrEmpty(_lang))
            {
                _lang = Constants.Localization.FALLBACK_LANG;
            }

            return _data.Configs[_lang].FileName;
        }

        public void SetLang(string lang)
        {
            lang = lang.ToUpper();

            _lang = _langs.Contains(lang) ? lang : Constants.Localization.FALLBACK_LANG;
        }

        public string Get(string alias)
        {
            if (string.IsNullOrEmpty(_lang))
            {
                _lang = Constants.Localization.FALLBACK_LANG;
            }

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
            _data = _resourceLoader.LoadLocal<LocalizationData>(Constants.Localization.FILE_NAME);
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
            var list = GameObject.FindObjectsOfType<Localization>(true).ToList();

            foreach (var item in list)
            {
                item.UpdateText();
            }
        }
    }
}
