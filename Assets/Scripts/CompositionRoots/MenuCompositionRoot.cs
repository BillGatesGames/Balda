using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MenuCompositionRoot : MonoBehaviour
    {
        [SerializeField]
        private MenuView _menuView;

        private IMenuPresenter _menu;

        void Start()
        {
            GameSettings.Load();
            LocalizationManager.Instance.LoadLocalization();
            LocalizationManager.Instance.SetLang(GameSettings.Lang);
            LocalizationManager.Instance.UpdateLocalization();

            _menu = new MenuPresenter(_menuView);
        }
    }
}
