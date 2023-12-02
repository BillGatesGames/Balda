using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IWordListModel
    {
        List<string> Words { get; }
        string GetTotalText();
        void Clear();
    }
}
