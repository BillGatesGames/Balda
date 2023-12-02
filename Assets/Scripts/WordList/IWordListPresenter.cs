using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWordListPresenter : IStateHandler
{
    void AddWord(string text);
}
