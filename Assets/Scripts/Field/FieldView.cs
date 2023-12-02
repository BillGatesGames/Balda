using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FieldView : MonoBehaviour, IFieldView
{
    [SerializeField]
    private GridLayoutGroup _gridLayout;

    [SerializeField]
    private Cell _cellPrefab;

    [SerializeField]
    private Transform _cellParent;

    private Dictionary<Vector2Int, Cell> _cells;

    private IFieldPresenter _presenter;

    public void Init(IFieldPresenter presenter)
    {
        _presenter = presenter;
    }

    public Cell Get(Vector2Int pos)
    {
        return _cells[pos];
    }

    public void UpdateView(char?[,] field)
    {
        int size = field.GetLength(0);

        Clear();

        AdjustSize(size);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var cell = Instantiate(_cellPrefab, _cellParent);
                cell.X = x;
                cell.Y = y;
                cell.Char = field[x, y];
                cell.OnClick = LetterClick; 

                _cells.Add(new Vector2Int(x, y), cell);
            }
        }
    }
    
    private void AdjustSize(int size)
    {
        var rect = _gridLayout.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size, size) * _gridLayout.cellSize;
    }

    public void UpdateSelection(IReadOnlyCollection<Vector2Int> selection)
    {
        foreach (var kvp in _cells)
        {
            kvp.Value.Select(selection.Contains(kvp.Key));
        }
    }

    public void ClearSelection()
    {
        foreach (var kvp in _cells)
        {
            kvp.Value.Select(false);
        }
    }

    public void LetterClick(Cell cell)
    {
        _presenter.CellClick(cell);
    }

    private void Clear()
    {
        if (_cells != null)
        {
            foreach (var cell in _cells.Values)
            {
                Destroy(cell.gameObject);
            }
        }

        _cells = new Dictionary<Vector2Int, Cell>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
