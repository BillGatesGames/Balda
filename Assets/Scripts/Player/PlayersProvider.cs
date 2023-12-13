using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IPlayersProvider
    {
        IPlayer Player1 { get; }
        IPlayer Player2 { get; }
    }

    public class PlayersProvider : IPlayersProvider
    {
        public IPlayer Player1 { get; }
        public IPlayer Player2 { get; }

        public PlayersProvider(IPlayer player1, IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }
}
