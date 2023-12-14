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
        protected SignalBus _signalBus;

        public MessagePresenter(IMessageModel model, IMessageView view, SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _signalBus = signalBus;
        }

        public virtual void Initialize()
        {
            _view.Init(this);

            _signalBus.TryUnsubscribe<StateData>(SwitchToState);
            _signalBus.Subscribe<StateData>(SwitchToState);
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
            _signalBus.Unsubscribe<StateData>(SwitchToState);
        }
    }
}
