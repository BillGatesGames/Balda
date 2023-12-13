using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class PlayersInstaller : MonoInstaller
    {
        [Inject]
        private IGameSettings _gameSettings;

        public override void InstallBindings()
        {
            var player1 = CreatePlayer(PlayerSide.Left, _gameSettings.Player1);
            var player2 = CreatePlayer(PlayerSide.Right, _gameSettings.Player2);
            var provider = new PlayersProvider(player1, player2);

            Container.Bind<IPlayersProvider>().To<PlayersProvider>().FromInstance(provider).AsSingle();
        }

        private IPlayer CreatePlayer(PlayerSide side, PlayerType type)
        {
            IPlayer player = type == PlayerType.Human ? Container.Instantiate<Human>() : Container.Instantiate<AI>();
            player.PlayerSide = side;
            return player; 
        }
    }
}
