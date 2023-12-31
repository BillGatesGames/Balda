using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IWordListPresenter : IPresenter, IStateHandler
    {
        void AddWord(string text);
        int GetScore();
    }
}
