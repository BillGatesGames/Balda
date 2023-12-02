using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IFieldModel
    {
        bool IsLocked { get; set; }
        bool IsFilled { get; }
        Vector2Int? LastCharPos { get; }
        Selection Selection { get; }
        IReadOnlyCollection<string> ExcludedWords { get; }
        Trie GetTrie();
        char?[,] GetField();
        int GetSize();
        void Init();
        void SetChar(Vector2Int pos, char @char);
        bool TrySetSelectedWord();
        bool IsEmpty(Vector2Int pos);
        void DeleteLastChar();
    }
}