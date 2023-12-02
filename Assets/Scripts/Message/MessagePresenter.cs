using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MessagePresenter : IMessagePresenter
    {
        public event Action OnLeftButtonClick;
        public event Action OnRightButtonClick;

        private IMessageModel _model;
        private IMessageView _view;

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

        public void SwitchToState(StateData data)
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
                        if (data.SubState == SubState.None)
                        {
                            return;
                        }

                        _view.SetData(_model.GetMessageData(data));

                        if (data.InputLocking)
                        {
                            _view.Hide();
                            return;
                        }
                    }
                    break;
                case State.Completed:
                    {
                        _view.SetData(_model.GetMessageData(data));
                    }
                    break;
            }
        }
    }
}
