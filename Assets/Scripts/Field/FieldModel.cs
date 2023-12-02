using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SelectionMode
{
    None,
    Single,
    Multiple
}

public class FieldModel : IFieldModel
{
    private const int DEFAULT_SIZE = 5;
    private const string SIZE_KEY = "field_size";

    private int _size;
    private char?[,] _field;
    private Trie _trie;
    private List<Vector2Int> _selected;
    private List<Vector2Int> _dxdy;
    private HashSet<string> _excludedWords;

    public bool IsLocked { get; set; } = false;

    public SelectionMode SelectionMode { get; set; } = SelectionMode.None;

    public IReadOnlyCollection<Vector2Int> Selection
    {
        get
        {
            return _selected;
        }
    }

    public IReadOnlyCollection<string> ExcludedWords
    {
        get
        {
            return _excludedWords;
        }
    }

    private Vector2Int? _lastCharPos;

    public Vector2Int? LastCharPos
    {
        get
        {
            return _lastCharPos;
        }
    }

    public bool IsFilled
    {
        get
        {
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (_field[x, y] == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public FieldModel()
    {
        int size = GetSize();

        if (size % 2 == 0)
        {
            throw new System.ArgumentException("Field size must be an odd number");
        }

        _size = size;
        _field = new char?[_size, _size];
        _trie = new Trie();
        _excludedWords = new HashSet<string>();
        _selected = new List<Vector2Int>();

        _dxdy = new List<Vector2Int>()
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };

        var words = _trie.Words.Where(w => w.Length == _size).ToList();

        if (words.Count == 0)
        {
            throw new System.Exception("No word found to initialize the field");
        }

        string word = words[UnityEngine.Random.Range(0, words.Count)];
      
        for (int x = 0; x < size; x++)
        {
            _field[x, size / 2] = word[x];
        }

        _excludedWords.Add(word);
    }

    public int GetSize()
    {
        /*
        if (PlayerPrefs.HasKey(SIZE_KEY))
        {
            return PlayerPrefs.GetInt(SIZE_KEY);
        }
        */
        return DEFAULT_SIZE;
    }

    public Trie GetTrie()
    {
        return _trie;
    }

    public char?[,] GetField()
    {
        return _field;
    }

    public string GetSelectedWord()
    {
        return new string(_selected.Select(pos => _field[pos.x, pos.y].Value).ToArray());
    }

    public bool TrySetSelectedWord()
    {
        if (!_lastCharPos.HasValue)
        {
            return false;
        }

        if (!_selected.Contains(_lastCharPos.Value))
        {
            return false;
        }

        string word = GetSelectedWord();

        if (!_trie.Words.Contains(word))
        {
            return false;
        }

        _excludedWords.Add(word);
        _lastCharPos = null;

        return true;
    }

    private bool CanSelectInMultipleMode(Vector2Int pos)
    {
        for (int i = 0; i < _dxdy.Count; i++)
        {
            int dx = pos.x + _dxdy[i].x;
            int dy = pos.y + _dxdy[i].y;

            if (InBounds(dx, dy))
            {
                if (_field[dx, dy] != null)
                {
                    bool selected = _selected.Contains(new Vector2Int(dx, dy));

                    if (selected)
                    {
                        return true;
                    } 
                }
            }
        }

        return false;
    }

    private bool CanSelectInSingleMode(Vector2Int pos)
    {
        if (_field[pos.x, pos.y] != null)
        {
            return false;
        }

        for (int i = 0; i < _dxdy.Count; i++)
        {
            int dx = pos.x + _dxdy[i].x;
            int dy = pos.y + _dxdy[i].y;

            if (InBounds(dx, dy))
            {
                if (_field[dx, dy] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsEmpty(Vector2Int pos)
    {
        return !_field[pos.x, pos.y].HasValue;
    }

    public bool CanSelect(Vector2Int pos)
    {
        switch (SelectionMode)
        {
            case SelectionMode.Single:
                {
                    return CanSelectInSingleMode(pos);
                }
            case SelectionMode.Multiple:
                {
                    if (_field[pos.x, pos.y] != null)
                    {
                        return _selected.Count == 0 || CanSelectInMultipleMode(pos);
                    }             
                }
                break;
        }

        return false;
    }

    public bool IsSelect(Vector2Int pos)
    {
        return _selected.Contains(pos);
    }

    public void Select(Vector2Int? pos)
    {
        if (!pos.HasValue)
        {
            _selected = new List<Vector2Int>();
            return;
        }

        if (SelectionMode == SelectionMode.Single)
        {
            _selected = new List<Vector2Int>() { pos.Value };
        }

        if (SelectionMode == SelectionMode.Multiple)
        {
            if (!_selected.Contains(pos.Value))
            {
                _selected.Add(pos.Value);
            }
        }
    }

    public void ClearSelection()
    {
        _selected = new List<Vector2Int>();
    }

    public void DeleteLastChar()
    {
        if (_lastCharPos.HasValue)
        {
            _field[_lastCharPos.Value.x, _lastCharPos.Value.y] = null;
        }

        _lastCharPos = null;
    }

    public void SetChar(Vector2Int pos, char @char)
    {
        _lastCharPos = pos;
        _field[pos.x, pos.y] = @char;
    }

    private bool InBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < _size && y < _size;
    }
}
