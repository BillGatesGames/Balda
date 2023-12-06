using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IFieldModel
    {
        bool InputLocked { get; set; }
        bool IsFilled { get; }
        Selection Selection { get; }
        IReadOnlyCollection<string> ExcludedWords { get; }
        Vector2Int? GetLastCharPos();
        Trie GetTrie();
        char?[,] GetField();
        int GetSize();
        void Init();
        void SetChar(Vector2Int pos, char? @char);
        bool TryAddSelectedWord();
        bool IsEmpty(Vector2Int pos);
        void DeleteLastChar();
    }
}