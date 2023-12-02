using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IMessagePresenter : IStateHandler
    {
        event Action OnOkClick;
        event Action OnResetClick;
        void LeftBtnClick();
        void RightBtnClick();
    }
}
