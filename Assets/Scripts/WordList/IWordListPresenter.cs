using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IWordListPresenter : IStateHandler
    {
        void AddWord(string text);
        int GetScore();
    }
}
