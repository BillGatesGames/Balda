using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

    public interface IStateProvider
    {
        State State { get; }
        SubState SubState { get; }
    }

    public class StateProvider : IStateProvider
    {
        private readonly IStateMachineModel _model;

        public State State => _model.State;
        public SubState SubState => _model.SubState;

        public StateProvider(IStateMachineModel model)
        {
            _model = model;
        }
    }
}
