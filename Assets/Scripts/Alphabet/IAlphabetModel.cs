using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlphabetModel
{
    IReadOnlyList<char> Chars { get; }

    bool IsLocked { get; set; }
}
