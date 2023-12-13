using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class Human : IPlayer
    {
        public event Action<IPlayer> OnLetterSet;
        public event Action<IPlayer> OnMoveCompleted;
        public event Action<IPlayer> OnResetMoveState;
        public event Action<IPlayer, SubState> OnError;

        private IFieldPresenter _field;
        private IMessagePresenter _message;
        private IAlphabetPresenter _alphabet;
        private IWordListPresenter _wordList;
        private Func<(State State, SubState SubState)> _stateProvider;

        public bool InputLocking => false;

        private Human() { }

        public Human(IFieldPresenter field, IAlphabetPresenter alphabet, IMessagePresenter message, IWordListPresenter wordList, Func<(State State, SubState SubState)> stateProvider)
        {
            _field = field;
            _alphabet = alphabet;
            _message = message;
            _wordList = wordList;
            _stateProvider = stateProvider;
        }

        private void Alphabet_OnCellClick(Cell cell)
        {
            if (_stateProvider().SubState == SubState.LetterSelection)
            {
                if (_field.GetModel().Selection.Positions.Count == 1)
                {
                    var pos = _field.GetModel().Selection.Positions.First();
                    _field.SetChar(pos, cell.Char.Value);
                }
            }
        }

        private void Message_OnLeftButtonClick()
        {
            switch (_stateProvider().SubState)
            {
                case SubState.LetterSelection:
                    {
                        if (_field.GetModel().GetLastCharPos().HasValue)
                        {
                            OnLetterSet?.Invoke(this);
                        }
                    }
                    break;
                case SubState.WordSelection:
                    {
                        if (_field.GetModel().Selection.Positions.Count == 0)
                        {
                            return;
                        }

                        if (_field.GetModel().TryAddSelectedWord())
                        {
                            var word = _field.GetModel().Selection.GetWord();
                            _wordList.AddWord(word);

                            Unsubscribe();

                            OnMoveCompleted?.Invoke(this);
                        }
                        else
                        {
                            OnError?.Invoke(this, SubState.WordNotExists);
                        }
                    }
                    break;
            }
        }

        private void Message_OnRightButtonClick()
        {
            if (_stateProvider().SubState != SubState.None)
            {
                OnResetMoveState?.Invoke(this);
            }
        }

        private void Subscribe()
        {
            Unsubscribe();

            _alphabet.OnCellClick += Alphabet_OnCellClick;
            _message.OnLeftButtonClick += Message_OnLeftButtonClick;
            _message.OnRightButtonClick += Message_OnRightButtonClick;
        }

        private void Unsubscribe()
        {
            _alphabet.OnCellClick -= Alphabet_OnCellClick;
            _message.OnLeftButtonClick -= Message_OnLeftButtonClick;
            _message.OnRightButtonClick -= Message_OnRightButtonClick;
        }

        public void Move()
        {
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}
