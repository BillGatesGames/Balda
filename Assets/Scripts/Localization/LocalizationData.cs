using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class Config
    {
        public string FileName;
        public string Alphabet;
    }

    [System.Serializable]
    public class LocalizationData
    {
        public Dictionary<string, Config> Configs = new();
        public Dictionary<string, Dictionary<string, string>> Localization = new();
    }
}