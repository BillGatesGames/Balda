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
        private IFieldModel _model;
        private List<Vector2Int> _dxdy;
        private List<Vector2Int> _positions;
        private char?[,] _field;

        public SelectionMode Mode { get; set; } = SelectionMode.None;

        public IReadOnlyCollection<Vector2Int> Positions
        {
            get
            {
                return _positions;
            }
        }

        public Selection(IFieldModel model)
        {
            _model = model;
            _field = _model.GetField();
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

        public void Select(Vector2Int pos)
        {
            bool canSelect = true;

            switch (Mode)
            {
                case SelectionMode.Single:
                    {
                        //если клетка пустая
                        if (_model.IsEmpty(pos))
                        {
                            //и ее нельзя выделить, то обнуляем позицию
                            if (!CanSelectInSingleMode(pos, _model.GetLastCharPos()))
                            {
                                canSelect = false;
                            }

                            //если есть новая буква, то удаляем
                            if (_model.GetLastCharPos().HasValue)
                            {
                                _model.DeleteLastChar();
                            }
                        }
                        else
                        {
                            //если есть новая буква
                            if (_model.GetLastCharPos().HasValue)
                            {
                                //она выделена 
                                if (_positions.Contains(_model.GetLastCharPos().Value))
                                {
                                    //и нажатая клетка не совпадает с выделенной, то обнуляем
                                    if (pos != _model.GetLastCharPos())
                                    {
                                        _model.DeleteLastChar();
                                        canSelect = false;
                                        
                                    }
                                }
                            }
                            else
                            {
                                _model.DeleteLastChar();
                                canSelect = false; 
                            }
                        }

                        Clear();

                        if (canSelect)
                        {
                            _positions.Add(pos);
                        }
                    }
                    break;
                case SelectionMode.Multiple:
                    {
                        //если нажатую клетку нелья выделить или она уже содержится в выделенных, то обнулям
                        if (!CanSelectInMultipleMode(pos) || _positions.Contains(pos))
                        {
                            canSelect = false;
                        }

                        if (canSelect)
                        {
                            if (!_positions.Contains(pos))
                            {
                                _positions.Add(pos);
                            }
                        }
                        else
                        {
                            Clear();
                        }
                    }
                    break;
            }
        }

        private bool CanSelectInMultipleMode(Vector2Int pos)
        {
            if (_field[pos.x, pos.y] != null)
            {
                if (_positions.Count == 0)
                {
                    return true;
                }
            }
            else
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

        private bool CanSelectInSingleMode(Vector2Int pos, Vector2Int? excludedPos)
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
                        if (excludedPos.HasValue)
                        {
                            if (excludedPos == new Vector2Int(dx, dy))
                            {
                                continue;
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        private bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _model.GetSize() && y < _model.GetSize();
        }
    }
}