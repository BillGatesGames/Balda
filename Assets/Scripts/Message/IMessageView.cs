using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IMessageView
{
    void Init(IMessagePresenter presenter);
    void Show(string text);
    void Hide();
}