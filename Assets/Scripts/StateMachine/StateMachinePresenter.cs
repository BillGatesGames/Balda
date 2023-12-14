using System;
using Zenject;

namespace Balda
{
    public class StateData
    {
        public bool InputLocking;
        public State State;
        public SubState SubState;

        public StateData(bool inputLocking, IStateMachineModel model)
        {
            InputLocking = inputLocking;
            State = model.State;
            SubState = model.SubState;
        }
    }

    public sealed class StateMachinePresenter : IStateMachinePresenter
    {
        private IStateMachineModel _model;
        private IFieldPresenter _field;
        private IMessagePresenter _message;
        private IPlayersProvider _players;
        private SignalBus _signalBus;

        public StateMachinePresenter(IStateMachineModel model, IFieldPresenter field, IMessagePresenter message, IPlayersProvider players, SignalBus signalBus) 
        { 
            _model = model;
            _field = field;
            _message = message;
            _players = players;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Subscribe();
            SwitchToInitState();
        }

        private void Subscribe()
        {
            Unsubscribe();

            _message.OnLeftButtonClick += MessageButtonClick;

            _players.Player1.OnResetMoveState += Player_OnResetMoveState;
            _players.Player2.OnResetMoveState += Player_OnResetMoveState;

            _players.Player1.OnLetterSet += Player_OnLetterSet;
            _players.Player2.OnLetterSet += Player_OnLetterSet;

            _players.Player1.OnError += Player_OnError;
            _players.Player2.OnError += Player_OnError;

            _players.Player1.OnMoveCompleted += Player_OnMoveCompleted;
            _players.Player2.OnMoveCompleted += Player_OnMoveCompleted;
        }

        private void Unsubscribe()
        {
            _message.OnLeftButtonClick -= MessageButtonClick;

            _players.Player1.OnResetMoveState -= Player_OnResetMoveState;
            _players.Player2.OnResetMoveState -= Player_OnResetMoveState;

            _players.Player1.OnLetterSet -= Player_OnLetterSet;
            _players.Player2.OnLetterSet -= Player_OnLetterSet;

            _players.Player1.OnError -= Player_OnError;
            _players.Player2.OnError -= Player_OnError;

            _players.Player1.OnMoveCompleted -= Player_OnMoveCompleted;
            _players.Player2.OnMoveCompleted -= Player_OnMoveCompleted;
        }

        public void MessageButtonClick()
        {
            if (_model.State == State.Completed || _model.SubState == SubState.WordNotFound)
            {
                SwitchToInitState();
            }
            else if (_model.SubState == SubState.WordNotExists)
            {
                SwitchToState(_model.State, SubState.LetterSelection);
            }
        }

        //zenject bug: initialize not auto called or I don't know something
        private void InitializeMessage()
        {
            _message.Initialize(); 
        }

        //zenject bug: initialize not auto called or I don't know something
        private void InitializePlayers()
        {
            _players.Player1.Initialize();
            _players.Player2.Initialize();
        }

        private void Player_OnResetMoveState(IPlayer player)
        {
            SwitchToState(_model.State, SubState.LetterSelection);
        }

        private void Player_OnLetterSet(IPlayer player)
        {
            SwitchToState(_model.State, SubState.WordSelection);
        }

        private void Player_OnError(IPlayer player, SubState error)
        {
            SwitchToState(_model.State, error);
        }

        private void Player_OnMoveCompleted(IPlayer player)
        {
            if (_field.GetModel().IsFilled)
            {
                SwitchToState(State.Completed, SubState.None);
                return;
            }

            if (player == _players.Player1)
            {
                SwitchToPlayer2MoveState();
            }
            else
            {
                SwitchToPlayer1MoveState();
            }
        }

        private IPlayer GetCurrentPlayer()
        {
            if (_model.State == State.Player1Move)
            {
                return _players.Player1;
            }

            if (_model.State == State.Player2Move)
            {
                return _players.Player2;
            }

            return null;
        }

        private void SwitchToInitState()
        {
            InitializeMessage();
            InitializePlayers();
            SwitchToState(State.Init, SubState.None);
            SwitchToPlayer1MoveState();
        }

        private void SwitchToPlayer1MoveState()
        {
            SwitchToState(State.Player1Move, SubState.LetterSelection);

            _players.Player1.Move();
        }

        private void SwitchToPlayer2MoveState()
        {
            SwitchToState(State.Player2Move, SubState.LetterSelection);

            _players.Player2.Move();
        }

        public void SwitchToState(State state, SubState subState)
        {
            _model.State = state;
            _model.SubState = subState;

            var player = GetCurrentPlayer();

            var data = new StateData(player != null ? player.InputLocking : true, _model);

            _signalBus.Fire(data);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}