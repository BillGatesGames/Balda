using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public sealed class WordListPresenter : IWordListPresenter, IDisposable
    {
        private IWordListModel _model;
        private IWordListView _view;
        private bool disposedValue;

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

        private void Clean()
        {
            if (!disposedValue)
            {
                _model = null;
                _view = null;

                EventBus.Unregister(this);

                disposedValue = true;
            }
        }

        ~WordListPresenter()
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
