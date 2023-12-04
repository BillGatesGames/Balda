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
            LocalizationManager.Instance.SetLang(GameSettings.Lang);
            LocalizationManager.Instance.UpdateLocalization();

            _root.Build();
        }
    }
}
