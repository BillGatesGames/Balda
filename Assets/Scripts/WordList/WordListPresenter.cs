using System.Linq;

namespace Balda
{
    public sealed class WordListPresenter : IWordListPresenter
    {
        private IWordListModel _model;
        private IWordListView _view;

        private WordListPresenter() { }

        public WordListPresenter(IWordListModel model, IWordListView view)
        {
            _model = model;
            _view = view;
            _view.Init(this);

            EventBus.Register(this);
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
            EventBus.Unregister(this);
        }
    }
}
