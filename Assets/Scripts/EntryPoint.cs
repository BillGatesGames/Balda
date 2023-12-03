using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CompositionRoot _compositionRoot;

        void Start()
        {
            LocalizationManager.Instance.LoadLocalization();
            LocalizationManager.Instance.SetLang(Constants.Localization.RU);
            LocalizationManager.Instance.UpdateLocalization();

            _compositionRoot.Build();
        }
    }
}
