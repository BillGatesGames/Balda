using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessagePresenter : IStateHandler
{
    event Action OnOkClick;
    event Action OnResetClick;
    void OkClick();
    void ResetClick();
}
