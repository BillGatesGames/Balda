using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IHandler { }

    public interface IStateHandler : IHandler
    {
        void SwitchToState(StateData data);
    }
}

