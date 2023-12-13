using System;
using Zenject;

namespace Balda
{
    public sealed class AlphabetPresenter : IAlphabetPresenter
    {
        public event Action<Cell> OnCellClick;

        [Inject]
        private IAlphabetView _view;

        [Inject]
        private IAlphabetModel _model;
        
        private bool disposedValue;

        public AlphabetPresenter()
        {

        }

        public void Initialize()
        {
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

        public void Dispose()
        {
            EventBus.Unregister(this);
        }
    }
}
