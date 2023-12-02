using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWordListModel
{
    List<string> Words { get; }
    string GetTotalText();
    void Clear();
}
