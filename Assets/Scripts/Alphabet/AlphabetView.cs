using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Balda
{
    public class AlphabetView : MonoBehaviour, IAlphabetView
    {
        [SerializeField]
        private GridLayoutGroup _gridLayout;

        [SerializeField]
        private Cell _cellPrefab;

        [SerializeField]
        private Transform _cellParent;

        private List<Cell> _cells;

        private IAlphabetPresenter _presenter;

        public void Init(IAlphabetPresenter presenter)
        {
            _presenter = presenter;
        }

        public void UpdateView(IReadOnlyList<char> chars)
        {
            Clear();

            var row1 = new List<char>();
            var row2 = new List<char>();

            for (int i = 0; i < chars.Count; i++)
            {
                if (i % 2 == 0)
                {
                    row1.Add(chars[i]);
                }
                else
                {
                    row2.Add(chars[i]);
                }
            }

            for (int i = 0; i < row1.Count; i++)
            {
                CreateCell(row1[i]);

                if (i < row2.Count)
                {
                    CreateCell(row2[i]);
                }
            }
        }

        public void LetterClick(Cell cell)
        {
            _presenter.CellClick(cell);
        }

        private void CreateCell(char @char)
        {
            var cell = Instantiate(_cellPrefab, _cellParent);
            cell.Char = @char;
            cell.OnClick = LetterClick;
        }

        private void Clear()
        {
            if (_cells != null)
            {
                foreach (var cell in _cells)
                {
                    Destroy(cell.gameObject);
                }
            }

            _cells = new List<Cell>();
        }

        void Start()
        {

        }
    }
}
