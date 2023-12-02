using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Balda
{
    public class AlphabetPresenter : IAlphabetPresenter
    {
        public event Action<Cell> OnCellClick;

        private IAlphabetView _view;
        private IAlphabetModel _model;

        public AlphabetPresenter(IAlphabetModel model, IAlphabetView view)
        {
            _model = model;

            _view = view;
            _view.Init(this);

            EventBus.Register(this);
        }

        public void CellClick(Cell cell)
        {
            if (_model.IsLocked)
            {
                return;
            }

            OnCellClick?.Invoke(cell);
        }

        public void SwitchToState(StateData data)
        {
            switch (data.State)
            {
                case State.Init:
                    {
                        _view.UpdateView(_model.Chars);
                    }
                    break;
                case State.Player1Move:
                case State.Player2Move:
                    {
                        _model.IsLocked = data.InputLocking;
                    }
                    break;
            }
        }
    }
}
