using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IMessageModel
    {
        string GetMessageText(SubState state);
        string GetButtonText();
    }
}
