using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Balda.Tests
{
    public class MultipleSelectionTest
    {
        private IFieldModel _model;
        private Vector2Int _letterPos;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return null;

            LocalizationManager.Instance.LoadLocalization();

            _model = new FieldModel(Constants.Field.SIZE_5x5);
            _model.Selection.Mode = SelectionMode.Multiple;
            _letterPos = new Vector2Int(0, 1);
        }

        private void SetLetterAndSelectWord()
        {
            _model.Selection.Clear();
            _model.SetChar(_letterPos, 'X');
            _model.Selection.Select(_letterPos);
            _model.Selection.Select(new Vector2Int(0, 2));
            _model.Selection.Select(new Vector2Int(1, 2));
        }

        private void UnsetLetter()
        {
            _model.SetChar(_letterPos, null);
        }

        [Test]
        public void SelectWord()
        {
            SetLetterAndSelectWord();

            Assert.AreEqual(3, _model.Selection.Positions.Count);

            UnsetLetter();
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать букву, которая не стоит
        /// рядом с какой-либо буквой выбранного слова
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectLetterThatIsNotLetterOfThisWord()
        {
            SetLetterAndSelectWord();

            _model.Selection.Select(new Vector2Int(4, 2));

            Assert.AreEqual(0, _model.Selection.Positions.Count);

            UnsetLetter();
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать букву в этом слове
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectLetterOfWord()
        {
            SetLetterAndSelectWord();

            _model.Selection.Select(new Vector2Int(0, 1));

            Assert.AreEqual(0, _model.Selection.Positions.Count);

            UnsetLetter();
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать пустую клетку, у которой 
        /// соседи с буквой
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectEmptyCellAroundOccupiedCells()
        {
            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells();

            foreach (var cell in cells)
            {
                SetLetterAndSelectWord();

                _model.Selection.Select(cell);

                Assert.AreEqual(0, _model.Selection.Positions.Count);

                UnsetLetter();
            }
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать пустую клетку, у которой 
        /// соседи с буквой
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectEmptyCell()
        {
            var cells = _model.GetField().GetEmptyCells();

            foreach (var cell in cells)
            {
                SetLetterAndSelectWord();

                _model.Selection.Select(cell);

                Assert.AreEqual(0, _model.Selection.Positions.Count);

                UnsetLetter();
            }
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return null;
        }
    }
}
