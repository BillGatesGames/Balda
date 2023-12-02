using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldPresenter : IFieldPresenter
{
    public event Action<Cell> OnCellClick;
    public event Action OnEnemyMoveComplete;

    private IFieldView _view;
    private IFieldModel _model;

    public FieldPresenter(IFieldModel model, IFieldView view)
    {
        _model = model;

        _view = view;
        _view.Init(this);

        EventBus.Register(this);
    }

    public IFieldModel GetModel()
    {
        return _model;
    }

    public IFieldView GetView()
    {
        return _view;
    }

    public void SetChar(Vector2Int pos, char @char, bool selected = false)
    {
        _model.SetChar(pos, @char);

        if (selected)
        {
            _model.Select(pos);
        }

        _view.UpdateView(_model.GetField());
        _view.UpdateSelection(_model.Selection);
    }

    public void CellClick(Cell cell)
    {
        if (_model.IsLocked)
        {
            return;
        }

        Vector2Int? pos = new Vector2Int(cell.X, cell.Y);

        switch (_model.SelectionMode)
        {
            case SelectionMode.Single:
                {
                    if (_model.Selection.Count == 1 && _model.LastCharPos != pos.Value)
                    {
                        _model.DeleteLastChar();
                        _view.UpdateView(_model.GetField());

                        if (!_model.CanSelect(pos.Value))
                        {
                            pos = null;
                        }
                    }
                    else if (!_model.CanSelect(pos.Value) || !_model.IsEmpty(pos.Value))
                    {
                        pos = null;
                    }

                    if (pos == null)
                    {
                        _model.DeleteLastChar();
                        _view.UpdateView(_model.GetField());
                    }

                    _model.Select(pos);
                }
                break;
            case SelectionMode.Multiple:
                {
                    if (!_model.CanSelect(pos.Value))
                    {
                        pos = null;
                    }

                    _model.Select(pos);
                }
                break;
        }

        _view.UpdateSelection(_model.Selection);

        OnCellClick?.Invoke(cell);
    }

    private void ClearSelection()
    {
        _model.ClearSelection();
        _view.UpdateSelection(_model.Selection);
    }

    public void SwitchToState(StateData data)
    {
        switch (data.State)
        {
            case State.Init: 
                {
                    _view.UpdateView(_model.GetField());
                }
                break;
            case State.Player1Move:
            case State.Player2Move:
                {
                    _model.IsLocked = data.InputLocking;

                    SwitchToSubState(data.SubState);
                    ClearSelection();
                }
                break;
            case State.Finish:
                {
                    ClearSelection();
                }
                break;
        }
    }

    public void SwitchToSubState(SubState state)
    {
        switch (state)
        {
            case SubState.LetterSelection:
                {
                    _model.SelectionMode = SelectionMode.Single;
                    _model.DeleteLastChar();
                    _view.UpdateView(_model.GetField());
                }
                break;
            case SubState.WordSelection:
                {
                    _model.SelectionMode = SelectionMode.Multiple;
                }
                break;
        }
    }

    public IEnumerator ShowWord(Word word, float delay, Action callback)
    {
        foreach (var letter in word.Letters)
        {
            yield return new WaitForSeconds(delay);

            _model.Select(letter.Pos);
            _view.UpdateSelection(_model.Selection);
        }

        _model.TrySetSelectedWord();

        yield return new WaitForSeconds(delay);

        callback?.Invoke();
    }
}
