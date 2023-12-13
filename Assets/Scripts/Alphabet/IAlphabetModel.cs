using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public interface IAlphabetModel : IInitializable
    {
        IReadOnlyList<char> Chars { get; }
        bool IsLocked { get; set; }
    }
}
