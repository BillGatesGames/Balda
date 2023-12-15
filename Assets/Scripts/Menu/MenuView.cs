using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Balda
{
    public class ButtonGroup
    {
        public event Action<ButtonEx> OnClick;

        private ButtonEx[] _buttons;
       
        private void ButtonClick(ButtonEx button)
        {
            OnClick?.Invoke(button);
        }

        public ButtonGroup(params ButtonEx[] buttons)
        {
            _buttons = buttons;

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].OnClick += ButtonClick;
            }
        }

        public void Select(ButtonEx button)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Selected = _buttons[i] == button;
            }
        }

        public void Clean()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].OnClick -= ButtonClick;
            }
        }

    }

    public class MenuView : MonoBehaviour, IMenuView
    {
        [SerializeField]
        private ButtonEx _langRU;

        [SerializeField]
        private ButtonEx _langEN;

        [SerializeField]
        private ButtonEx _size5x5;

        [SerializeField]
        private ButtonEx _size7x7;

        [SerializeField]
        private ButtonEx _size9x9;

        [SerializeField]
        private ButtonEx _player1Human;

        [SerializeField]
        private ButtonEx _player1AI;

        [SerializeField]
        private ButtonEx _player2Human;

        [SerializeField]
        private ButtonEx _player2AI;

        [SerializeField]
        private ButtonEx _play;

        private ButtonGroup _langGroup;
        private ButtonGroup _sizeGroup;
        private ButtonGroup _player1Group;
        private ButtonGroup _player2Group;

        private IMenuPresenter _presenter;

        public void Init(IMenuPresenter presenter)
        {
            _presenter = presenter;
            _play.OnClick = Play_OnClick;
        }

        private void Play_OnClick(ButtonEx button)
        {
            _presenter.PlayClick();
        }

        private void LangGroup_OnClick(ButtonEx button)
        {
            string lang = Constants.Localization.EN;

            if (button == _langEN)
            {
                lang = Constants.Localization.EN;
            }
            else if (button == _langRU)
            {
                lang = Constants.Localization.RU;
            }

            _presenter.LangClick(lang);
        }

        private void SizeGroup_OnClick(ButtonEx button)
        {
            int size = Constants.Field.SIZE_5x5;

            if (button == _size5x5)
            {
                size = Constants.Field.SIZE_5x5;
            }
            else if (button == _size7x7)
            {
                size = Constants.Field.SIZE_7x7;
            }
            else if (button == _size9x9)
            {
                size = Constants.Field.SIZE_9x9;
            }

            _presenter.SizeClick(size);
        }

        private void Player1Group_OnClick(ButtonEx button)
        {
            var player = PlayerType.Human;

            if (button == _player1AI)
            {
                player = PlayerType.AI;
            }

            _presenter.Player1TypeClick(player);
        }

        private void Player2Group_OnClick(ButtonEx button)
        {
            var player = PlayerType.Human;

            if (button == _player2AI)
            {
                player = PlayerType.AI;
            }

            _presenter.Player2TypeClick(player);
        }

        private void UpdateLang(string lang)
        {
            if (_langGroup == null)
            {
                _langGroup = new ButtonGroup(_langRU, _langEN);
                _langGroup.OnClick += LangGroup_OnClick;
            }

            switch (lang)
            {
                case Constants.Localization.RU:
                    _langGroup.Select(_langRU);
                    break;
                case Constants.Localization.EN:
                    _langGroup.Select(_langEN);
                    break;
            }
        }

        private void UpdateSize(int size)
        {
            if (_sizeGroup == null)
            {
                _sizeGroup = new ButtonGroup(_size5x5, _size7x7, _size9x9);
                _sizeGroup.OnClick += SizeGroup_OnClick;
            }

            switch (size)
            {
                case Constants.Field.SIZE_5x5:
                    _sizeGroup.Select(_size5x5);
                    break;
                case Constants.Field.SIZE_7x7:
                    _sizeGroup.Select(_size7x7);
                    break;
                case Constants.Field.SIZE_9x9:
                    _sizeGroup.Select(_size9x9);
                    break;
            }
        }

        private void UpdatePlayer1Type(PlayerType playerType)
        {
            if (_player1Group == null)
            {
                _player1Group = new ButtonGroup(_player1Human, _player1AI);
                _player1Group.OnClick += Player1Group_OnClick;
            }

            switch (playerType)
            {
                case PlayerType.Human:
                    _player1Group.Select(_player1Human);
                    break;
                case PlayerType.AI:
                    _player1Group.Select(_player1AI);
                    break;
            }
        }

        private void UpdatePlayer2Type(PlayerType playerType)
        {
            if (_player2Group == null)
            {
                _player2Group = new ButtonGroup(_player2Human, _player2AI);
                _player2Group.OnClick += Player2Group_OnClick;
            }

            switch (playerType)
            {
                case PlayerType.Human:
                    _player2Group.Select(_player2Human);
                    break;
                case PlayerType.AI:
                    _player2Group.Select(_player2AI);
                    break;
            }
        }

        public void UpdateView(IGameSettings settings)
        {
            UpdateLang(settings.Lang);
            UpdateSize(settings.Size);
            UpdatePlayer1Type(settings.Player1);
            UpdatePlayer2Type(settings.Player2);
        }

        void Start()
        {

        }

        private void OnDestroy()
        {
            _langGroup.Clean();
            _sizeGroup.Clean();
            _player1Group.Clean();
            _player2Group.Clean();
        }
    }
}
