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
        Completed
    }

    public enum SubState
    {
        None,
        LetterSelection,
        WordSelection,
        //errors
        WordNotExists,
        WordNotFound
    }

    public interface IStateMachineModel
    {
        public State State { get; set; }
        public SubState SubState { get; set; }
    }
}
