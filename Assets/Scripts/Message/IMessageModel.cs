using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IMessageModel
    {
        MessageData GetMessageData(StateData data);
    }
}
