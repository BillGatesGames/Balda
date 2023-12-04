using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private GameCompositionRoot _root;

        void Start()
        {
            var settings = new GameSettings();
            LocalizationManager.Instance.LoadLocalization();
            LocalizationManager.Instance.SetLang(settings.Lang);
            LocalizationManager.Instance.UpdateLocalization();

            _root.Build(settings);
        }
    }
}
