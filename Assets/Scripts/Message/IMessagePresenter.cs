using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public interface IMessagePresenter : IPresenter, IStateHandler
    {
        event Action OnLeftButtonClick;
        event Action OnRightButtonClick;

        void LeftBtnClick();
        void RightBtnClick();
    }
}
