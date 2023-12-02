using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Human : IPlayer
{
    public event Action<IPlayer> OnLetterSet;
    public event Action<IPlayer> OnMoveCompleted;
    public event Action<IPlayer> OnResetMoveState;
    public event Action<IPlayer, Error> OnError;

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

        _alphabet.OnCellClick += Alphabet_OnCellClick;
        _message.OnOkClick += Message_OnOKClick;
        _message.OnResetClick += Message_OnResetClick;
    }

    private void Alphabet_OnCellClick(Cell cell)
    {
        if (_stateProvider().SubState == SubState.LetterSelection)
        {
            if (_field.GetModel().Selection.Count == 1)
            {
                var pos = _field.GetModel().Selection.First();
                _field.SetChar(pos, cell.Char.Value);
            }
        }
    }

    private void Message_OnOKClick()
    {
        switch (_stateProvider().SubState)
        {
            case SubState.LetterSelection:
                {
                    if (_field.GetModel().LastCharPos.HasValue)
                    {
                        OnLetterSet?.Invoke(this);
                    }
                }
                break;
            case SubState.WordSelection:
                {
                    if (_field.GetModel().TrySetSelectedWord())
                    {
                        var word = _field.GetModel().GetSelectedWord();
                        _wordList.AddWord(word);

                        OnMoveCompleted?.Invoke(this);
                    }
                    else
                    {
                        OnError?.Invoke(this, Error.WordNotExists);
                    }
                }
                break;
        }
    }


    private void Message_OnResetClick()
    {
        if (_stateProvider().SubState != SubState.None)
        {
            OnResetMoveState?.Invoke(this);
        }
    }

    public void Move()
    {
        
    }
}
