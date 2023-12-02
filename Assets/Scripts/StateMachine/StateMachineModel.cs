using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineModel : IStateMachineModel
{
    private State _state;
    public State State 
    { 
        get
        {
            return _state;
        }

        set
        {
            _state = value;

            //EventBus.RaiseEvent<IStateHandler>(h => h.Handle(_state));
        }
    }

    private SubState _subState;
    public SubState SubState
    {
        get
        {
            return _subState;
        }

        set
        {
            _subState = value;

            //EventBus.RaiseEvent<ISubStateHandler>(h => h.Handle(_subState));
        }
    }
}
