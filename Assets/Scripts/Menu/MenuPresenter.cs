using System;
using Zenject;

namespace Balda
{
    public sealed class MenuPresenter : IMenuPresenter
    {
        private IMenuView _view;
        private IGameSettings _settings;
        private ILocalizationManager _localizationManager;
        private SignalBus _signalBus;

        public MenuPresenter(IMenuView view, IGameSettings settings, ILocalizationManager localizationManager, SignalBus signalBus) 
        { 
            _view = view;
            _settings = settings;
            _localizationManager = localizationManager;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _view.Init(this);

            UpdateView();
        }

        public void PlayClick()
        {
            _signalBus.Fire(Constants.Scenes.GAME);
        }

        public void LangClick(string lang)
        {
            _settings.Lang = lang;
            _settings.Save();

            _localizationManager.SetLang(_settings.Lang);
            _localizationManager.UpdateLocalization();

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
