using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IFieldPresenter : IStateHandler
    {
        IEnumerator ShowWord(Word word, float delay, Action callback);
        void SetChar(Vector2Int pos, char @char, bool select = false);
        void CellClick(Cell cell);
        IFieldModel GetModel();
        IFieldView GetView();
    }
}

