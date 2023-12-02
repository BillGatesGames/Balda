using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePresenter : IMessagePresenter
{
    public event Action OnOkClick;
    public event Action OnResetClick;

    private IMessageModel _model;
    private IMessageView _view;

    public MessagePresenter(IMessageModel model, IMessageView view)
    {
        _model = model;
        _view = view;
        _view.Init(this);

        EventBus.Register(this);
    }

    public void OkClick()
    {
        OnOkClick?.Invoke();
    }

    public void ResetClick()
    {
        OnResetClick?.Invoke();    
    }

    public void SwitchToState(StateData data)
    {
        switch (data.State)
        {
            case State.Init:
            case State.Finish:
                {
                    _view.Hide();
                }
                break;
            case State.Player1Move:
            case State.Player2Move:
                {
                    if (data.SubState == SubState.None)
                    {
                        return;
                    }

                    if (data.InputLocking)
                    {
                        _view.Hide();
                        return;
                    }

                    _view.Show(_model.GetMessageText(data.SubState));
                }
                break;
        }
    }

}
