using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class StateMachineModel : IStateMachineModel
    {
        public State State { get; set; }

        public SubState SubState { get; set; }
    }
}
