using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public class StateMachinePresenter : IStateMachinePresenter
{
    private IStateMachineModel _model;
    private IFieldPresenter _field;

    private IPlayer _player1;
    private IPlayer _player2;

    private StateMachinePresenter() { }

    public StateMachinePresenter(IPlayer player1, IPlayer player2, IFieldPresenter field, IStateMachineModel model)
    {
        _field = field;
        _model = model;

        _player1 = player1;
        _player2 = player2;

        _player1.OnResetMoveState += Player_OnResetMoveState;
        _player2.OnResetMoveState += Player_OnResetMoveState;

        _player1.OnLetterSet += Player_OnLetterSet;
        _player2.OnLetterSet += Player_OnLetterSet;

        _player1.OnError += Player_OnError;
        _player2.OnError += Player_OnError;

        _player1.OnMoveCompleted += Player_OnMoveCompleted;
        _player2.OnMoveCompleted += Player_OnMoveCompleted;

        SwitchToInitState();
        SwitchToPlayer1MoveState();
    }

    private void Player_OnResetMoveState(IPlayer player)
    {
        SwitchToState(_model.State, SubState.LetterSelection);
    }

    private void Player_OnLetterSet(IPlayer player)
    {
        SwitchToState(_model.State, SubState.WordSelection);
    }

    private void Player_OnError(IPlayer player, Error error)
    {
        if (error == Error.WordNotFound)
        {
            SwitchToState(State.Finish, SubState.None);
        }
    }

    private void Player_OnMoveCompleted(IPlayer player)
    {
        if (_field.GetModel().IsFilled)
        {
            SwitchToState(State.Finish, SubState.None);
            return;
        }

        if (player == _player1)
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
            return _player1;
        }

        if (_model.State == State.Player2Move)
        {
            return _player2;
        }

        return null;
    }

    private void SwitchToInitState()
    {
        SwitchToState(State.Init, SubState.None);
    }

    private void SwitchToPlayer1MoveState()
    {
        SwitchToState(State.Player1Move, SubState.LetterSelection);

        _player1.Move();
    }

    private void SwitchToPlayer2MoveState()
    {
        SwitchToState(State.Player2Move, SubState.LetterSelection);

        _player2.Move();
    }

    public void SwitchToState(State state, SubState subState)
    {
        _model.State = state;
        _model.SubState = subState;

        var player = GetCurrentPlayer();
        var data = new StateData(player != null ? player.InputLocking : true, _model);

        EventBus.RaiseEvent<IStateHandler>(h =>
        {
            h.SwitchToState(data);
        });
    }

    public void SwitchToState(StateData data)
    {
        
    }
}