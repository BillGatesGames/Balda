using System;

namespace Balda
{
    public sealed class AlphabetPresenter : IAlphabetPresenter, IDisposable
    {
        public event Action<Cell> OnCellClick;

        private IAlphabetView _view;
        private IAlphabetModel _model;
        private bool disposedValue;

        private AlphabetPresenter() { }

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

        private void Clean()
        {
            if (!disposedValue)
            {
                EventBus.Unregister(this);
                
                disposedValue = true;
            }
        }

        ~AlphabetPresenter()
        {
            Clean();
        }

        public void Dispose()
        {
            Clean();
            GC.SuppressFinalize(this);
        }
    }
}
