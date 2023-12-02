using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public enum SelectionMode
    {
        None,
        Single,
        Multiple
    }


    public class Selection
    {
        private char?[,] _field;
        private List<Vector2Int> _dxdy;
        private List<Vector2Int> _positions;

        public SelectionMode Mode { get; set; } = SelectionMode.None;

        public IReadOnlyCollection<Vector2Int> Positions
        {
            get
            {
                return _positions;
            }
        }

        public Selection(char?[,] field)
        {
            _field = field;
            _positions = new List<Vector2Int>();
            _dxdy = new List<Vector2Int>()
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };
        }

        public string GetWord()
        {
            return new string(_positions.Select(pos => _field[pos.x, pos.y].Value).ToArray());
        }

        public void Clear()
        {
            _positions = new List<Vector2Int>();
        }

        public void Select(Vector2Int? pos)
        {
            if (!pos.HasValue)
            {
                _positions = new List<Vector2Int>();
                return;
            }

            if (Mode == SelectionMode.Single)
            {
                _positions = new List<Vector2Int>() { pos.Value };
            }

            if (Mode == SelectionMode.Multiple)
            {
                if (!_positions.Contains(pos.Value))
                {
                    _positions.Add(pos.Value);
                }
            }
        }

        public bool CanSelect(Vector2Int pos)
        {
            switch (Mode)
            {
                case SelectionMode.Single:
                    {
                        return CanSelectInSingleMode(pos);
                    }
                case SelectionMode.Multiple:
                    {
                        if (_field[pos.x, pos.y] != null)
                        {
                            return _positions.Count == 0 || CanSelectInMultipleMode(pos);
                        }
                    }
                    break;
            }

            return false;
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
                        bool selected = _positions.Contains(new Vector2Int(dx, dy));

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

        private bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _field.GetLength(0) && y < _field.GetLength(1);
        }
    }
}