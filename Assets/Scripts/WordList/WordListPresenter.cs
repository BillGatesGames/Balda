using System.Linq;
using Zenject;

namespace Balda
{
    public sealed class WordListPresenter : IWordListPresenter
    {
        private IWordListModel _model;
        private IWordListView _view;
        private SignalBus _signalBus;

        public WordListPresenter(IWordListModel model, IWordListView view, SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.TryUnsubscribe<StateData>(SwitchToState);
            _signalBus.Subscribe<StateData>(SwitchToState);
        }

        public void AddWord(string text)
        {
            _model.Words.Insert(0, text);

            UpdateView();
        }

        public int GetScore()
        {
            return _model.Words.Sum(w => w.Length);
        }

        public void SwitchToState(StateData data)
        {
            switch (data.State)
            {
                case State.Init:
                    {
                        _model.Init();
                    }
                    break;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            _view.UpdateView(_model.Words, _model.GetTotalText());
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StateData>(SwitchToState);
        }
    }
}
