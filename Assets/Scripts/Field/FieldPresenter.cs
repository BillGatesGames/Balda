using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public sealed class FieldPresenter : IFieldPresenter
    {
        public event Action<Cell> OnCellClick;

        private IFieldView _view;
        private IFieldModel _model;

        private FieldPresenter() { }

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
                _model.Selection.Select(pos);
            }

            _view.UpdateView(_model.GetField());
            _view.UpdateSelection(_model.Selection.Positions);
        }

        public void CellClick(Cell cell)
        {
            if (_model.InputLocked)
            {
                return;
            }

            var pos = new Vector2Int(cell.X, cell.Y);

            _model.Selection.Select(pos);
            _view.UpdateView(_model.GetField());
            _view.UpdateSelection(_model.Selection.Positions);

            OnCellClick?.Invoke(cell);
        }

        private void ClearSelection()
        {
            _model.Selection.Clear();
            _view.UpdateSelection(_model.Selection.Positions);
        }

        public void SwitchToState(StateData data)
        {
            switch (data.State)
            {
                case State.Init:
                    {
                        _model.Init();
                        _view.UpdateView(_model.GetField());
                    }
                    break;
                case State.Player1Move:
                case State.Player2Move:
                    {
                        _model.InputLocked = data.InputLocking;

                        SwitchToSubState(data.SubState);

                        if (data.SubState != SubState.WordNotExists)
                        {
                            ClearSelection();
                        }
                    }
                    break;
                case State.Completed:
                    {
                        ClearSelection();
                    }
                    break;
            }
        }

        private void SwitchToSubState(SubState state)
        {
            switch (state)
            {
                case SubState.LetterSelection:
                    {
                        _model.Selection.Mode = SelectionMode.Single;
                        _model.DeleteLastChar();
                        _view.UpdateView(_model.GetField());
                    }
                    break;
                case SubState.WordSelection:
                    {
                        _model.Selection.Mode = SelectionMode.Multiple;
                    }
                    break;
            }
        }

        public IEnumerator ShowWord(Word word, float delay, Action callback)
        {
            foreach (var letter in word.Letters)
            {
                yield return new WaitForSeconds(delay);

                _model.Selection.Select(letter.Pos);
                _view.UpdateSelection(_model.Selection.Positions);
            }

            _model.TryAddSelectedWord();

            yield return new WaitForSeconds(delay);

            callback?.Invoke();
        }

        public void Dispose()
        {
            EventBus.Unregister(this);
        }
    }
}
