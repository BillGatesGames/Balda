using Zenject;

namespace Balda
{
    public sealed class TopMessagePresenter : MessagePresenter
    {
        public TopMessagePresenter(IMessageModel model, IMessageView view, SignalBus signalBus) : base(model, view, signalBus)
        {
        }

        public override void SwitchToState(StateData data)
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
    }
}
