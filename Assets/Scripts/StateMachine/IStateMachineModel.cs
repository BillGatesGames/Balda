using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public enum State
    {
        Init,
        Player1Move,
        Player2Move,
        Finish
    }

    public enum SubState
    {
        None,
        LetterSelection,
        WordSelection
    }

    public interface IStateMachineModel
    {
        public State State { get; set; }
        public SubState SubState { get; set; }
    }
}
