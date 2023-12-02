using System.Collections.Generic;
using UnityEngine;

public interface IFieldModel
{
    bool IsLocked { get; set; }
    bool IsFilled { get; }
    Vector2Int? LastCharPos { get; }
    SelectionMode SelectionMode { get; set; }
    IReadOnlyCollection<Vector2Int> Selection { get; }
    IReadOnlyCollection<string> ExcludedWords { get; }
    int GetSize();
    void SetChar(Vector2Int pos, char @char);
    char?[,] GetField();
    Trie GetTrie();
    string GetSelectedWord();
    bool TrySetSelectedWord();
    bool IsEmpty(Vector2Int pos);
    bool CanSelect(Vector2Int pos);
    bool IsSelect(Vector2Int pos);
    void Select(Vector2Int? pos);
    void ClearSelection();
    void DeleteLastChar();
}