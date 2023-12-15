using System;

namespace Balda
{
    public sealed class MenuPresenter : IMenuPresenter
    {
        private IMenuView _view;
        private IGameSettings _settings;

        private MenuPresenter() { }

        public MenuPresenter(IMenuView view, IGameSettings settings)
        {
            _settings = settings;
            _view = view;
            _view.Init(this);

            UpdateView();
        }

        public void PlayClick()
        {
            EventBus.RaiseEvent<ISceneLoadHandler>(h => h.Load(Constants.Scenes.GAME));
        }

        public void LangClick(string lang)
        {
            _settings.Lang = lang;
            _settings.Save();

            LocalizationManager.Instance.SetLang(_settings.Lang);
            LocalizationManager.Instance.UpdateLocalization();

            UpdateView();
        }

        public void Player1TypeClick(PlayerType type)
        {
            _settings.Player1 = type;
            _settings.Save();

            UpdateView();
        }

        public void Player2TypeClick(PlayerType type)
        {
            _settings.Player2 = type;
            _settings.Save();

            UpdateView();
        }

        public void SizeClick(int size)
        {
            _settings.Size = size;
            _settings.Save();

            UpdateView();
        }

        private void UpdateView()
        {
            _view.UpdateView(_settings);
        }

        public void Dispose()
        {
            
        }
    }
}
