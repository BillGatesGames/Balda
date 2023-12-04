using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MessagePresenter : IMessagePresenter, IDisposable
    {
        public event Action OnLeftButtonClick;
        public event Action OnRightButtonClick;

        protected IMessageModel _model;
        protected IMessageView _view;
        private bool disposedValue;

        private MessagePresenter() { }

        public MessagePresenter(IMessageModel model, IMessageView view)
        {
            _model = model;
            _view = view;
            _view.Init(this);

            EventBus.Register(this);
        }

        public void LeftBtnClick()
        {
            OnLeftButtonClick?.Invoke();
        }

        public void RightBtnClick()
        {
            OnRightButtonClick?.Invoke();
        }

        public virtual void SwitchToState(StateData data)
        {
            switch (data.State)
            {
                case State.Init:
                    {
                        _view.Hide();
                    }
                    break;
                case State.Player1Move:
                case State.Player2Move:
                    {
                        _view.Hide();

                        if (!data.InputLocking && (data.SubState == SubState.LetterSelection || data.SubState == SubState.WordSelection))
                        {
                            _view.SetData(_model.GetMessageData(data));
                        } 
                    }
                    break;
                case State.Completed:
                    {
                        _view.Hide();
                    }
                    break;
            }
        }

        protected virtual void Clean()
        {
            if (!disposedValue)
            {
                EventBus.Unregister(this);

                disposedValue = true;
            }
        }

        ~MessagePresenter()
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
