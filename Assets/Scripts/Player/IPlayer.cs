using System;
using System.Collections;

namespace Balda
{
    public interface IPlayer
    {
        event Action<IPlayer> OnLetterSet;
        event Action<IPlayer> OnMoveCompleted;
        event Action<IPlayer> OnResetMoveState;
        event Action<IPlayer, Error> OnError;
        bool InputLocking { get; }
        void Move();
    }
}