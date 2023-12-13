using System;
using System.Collections;
using Zenject;

namespace Balda
{
    public enum PlayerType
    {
        Human,
        AI
    }

    public enum PlayerSide
    {
        Left,
        Right
    }

    public interface IPlayer : IInitializable, IDisposable
    {
        event Action<IPlayer> OnLetterSet;
        event Action<IPlayer> OnMoveCompleted;
        event Action<IPlayer> OnResetMoveState;
        event Action<IPlayer, SubState> OnError;
        PlayerSide PlayerSide { get; set; }
        bool InputLocking { get; }
        void Move();
    }
}