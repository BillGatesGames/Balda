using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Balda
{
    public interface IMessageView
    {
        void Init(IMessagePresenter presenter);
        void SetData(MessageData data);
        void Hide();
    }
}