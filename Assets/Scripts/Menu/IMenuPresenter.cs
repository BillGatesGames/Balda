namespace Balda
{
    public interface IMenuPresenter : IPresenter
    {
        void LangClick(string lang);
        void SizeClick(int size);
        void Player1TypeClick(PlayerType type);
        void Player2TypeClick(PlayerType type);
        void PlayClick();
    }
}
