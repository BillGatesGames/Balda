using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class DebugController : MonoBehaviour
    {
        [SerializeField]
        private Transform _cellParent;

        [SerializeField]
        private DebugCell _cellPrefab;

        public void CreateCell(Vector2 pos, string text, Color color)
        {
            var cell = Instantiate(_cellPrefab, _cellParent);
            var rect = cell.GetComponent<RectTransform>();
            rect.anchoredPosition = pos;

            cell.SetText(text);
            cell.SetColor(color);
        }

        public void Clear()
        {
            var cells = FindObjectsOfType<DebugCell>().ToList();
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
        }

        public void ShowTraversal(IList<Word> words, Func<Word, bool> wordExists, Func<Vector2Int, Cell> getCell)
        {
            StartCoroutine(StartTraversal(words, wordExists, getCell));
        }

        private IEnumerator StartTraversal(IList<Word> words, Func<Word, bool> wordExists, Func<Vector2Int, Cell> getCell)
        {
            foreach (Word word in words)
            {
                yield return null;
                yield return null;

                Clear();

                Color color = wordExists(word) ? Color.green : Color.red;

                for (int i = 0; i < word.Letters.Count; i++)
                {
                    var letter = word.Letters[i];
                    var cell = getCell(letter.Pos);
                    var rect = cell.GetComponent<RectTransform>();
                    CreateCell(rect.position, letter.Char.ToString(), i == 0 ? Color.cyan : color);
                }
            }

            Clear();
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
