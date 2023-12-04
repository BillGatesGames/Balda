using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MenuCompositionRoot : MonoBehaviour
    {
        [SerializeField]
        private MenuView _menuView;

        void Start()
        {
            var settings = new GameSettings();
            LocalizationManager.Instance.LoadLocalization();
            LocalizationManager.Instance.SetLang(settings.Lang);
            LocalizationManager.Instance.UpdateLocalization();

            var menu = new MenuPresenter(_menuView, settings);
        }
    }
}
