namespace Balda
{
    public interface IMessageView
    {
        void Init(IMessagePresenter presenter);
        void SetData(MessageData data);
        void Hide();
    }
}