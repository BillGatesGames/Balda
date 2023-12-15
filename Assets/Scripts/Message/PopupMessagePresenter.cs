namespace Balda
{
    public sealed class PopupMessagePresenter : MessagePresenter
    {
        public PopupMessagePresenter(IMessageModel model, IMessageView view) : base(model, view) 
        { 
            
        }

        public override void SwitchToState(StateData data)
        {
            if (data.State == State.Completed || data.SubState == SubState.WordNotExists || data.SubState == SubState.WordNotFound)
            {
                _view.SetData(_model.GetMessageData(data));
            }
            else
            {
                _view.Hide();
            }
        }
    }
}
