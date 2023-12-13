using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public interface IFieldModel : IInitializable
    {
        bool InputLocked { get; set; }
        bool IsFilled { get; }
        Selection Selection { get; }
        IReadOnlyCollection<string> ExcludedWords { get; }
        Vector2Int? GetLastCharPos();
        ITrie GetTrie();
        char?[,] GetField();
        int GetSize();
        void SetChar(Vector2Int pos, char? @char);
        bool TryAddSelectedWord();
        bool IsEmpty(Vector2Int pos);
        void DeleteLastChar();
    }
}