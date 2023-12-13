using System;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class MessagePresenter : IMessagePresenter
    {
        public event Action OnLeftButtonClick;
        public event Action OnRightButtonClick;

        protected IMessageModel _model;
        protected IMessageView _view;

        public MessagePresenter(IMessageModel model, IMessageView view)
        {
            _model = model;
            _view = view;
        }

        public virtual void Initialize()
        {
            _view.Init(this);

            EventBus.Register(this);
        }

        public virtual void LeftBtnClick()
        {
            OnLeftButtonClick?.Invoke();
        }

        public virtual void RightBtnClick()
        {
            OnRightButtonClick?.Invoke();
        }

        public virtual void SwitchToState(StateData data)
        {

        }

        public virtual void Dispose()
        {
            EventBus.Unregister(this);
        }
    }
}
