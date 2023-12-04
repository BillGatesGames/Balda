namespace Balda
{
    public interface IGameSettings
    {
        string Lang { get; set; }
        PlayerType Player1 { get; set; }
        PlayerType Player2 { get; set; }
        int Size { get; set; }

        void Save();
    }
}