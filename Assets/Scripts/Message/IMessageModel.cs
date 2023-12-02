using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessageModel
{
    string GetMessageText(SubState state);
    string GetButtonText();
}
