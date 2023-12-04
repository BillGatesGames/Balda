using System;

namespace Balda
{
    public class MenuPresenter : IMenuPresenter
    {
        private IMenuView _view;

        private MenuPresenter() { }

        public MenuPresenter(IMenuView view)
        {
            _view = view;
            _view.Init(this);
            _view.UpdateView();
        }

        public void LangClick(string lang)
        {
            GameSettings.Lang = lang;
            GameSettings.Save();

            LocalizationManager.Instance.SetLang(GameSettings.Lang);
            LocalizationManager.Instance.UpdateLocalization();

            _view.UpdateView();
        }

        public void Player1TypeClick(PlayerType type)
        {
            GameSettings.Player1 = type;
            GameSettings.Save();

            _view.UpdateView();
        }

        public void Player2TypeClick(PlayerType type)
        {
            GameSettings.Player2 = type;
            GameSettings.Save();

            _view.UpdateView();
        }

        public void SizeClick(int size)
        {
            GameSettings.Size = size;
            GameSettings.Save();

            _view.UpdateView();
        }
    }
}
