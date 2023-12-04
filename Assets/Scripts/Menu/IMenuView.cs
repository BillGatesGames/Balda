namespace Balda
{
    public interface IMenuView
    {
        void Init(IMenuPresenter presenter);
        void UpdateView();
    }
}