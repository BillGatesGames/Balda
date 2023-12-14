using System;
using Zenject;

namespace Balda
{
    public sealed class AlphabetPresenter : IAlphabetPresenter
    {
        public event Action<Cell> OnCellClick;

        private IAlphabetView _view;
        private IAlphabetModel _model;
        private SignalBus _signalBus;

        public AlphabetPresenter(IAlphabetModel model, IAlphabetView view, SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _view.Init(this);

            _signalBus.Subscribe<StateData>(SwitchToState);
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
            _signalBus.Unsubscribe<StateData>(SwitchToState);
        }
    }
}
