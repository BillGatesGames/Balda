using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Balda;

namespace Balda.Tests
{
    public class SingleSelectionTest
    {
        private IFieldModel _model;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return null;

            LocalizationManager.Instance.LoadLocalization();

            _model = new FieldModel(Constants.Field.SIZE_5x5);
            _model.Selection.Mode = SelectionMode.Single;
        }

        [Test]
        public void SelectEmptyPosAroundOccupiedPos()
        {
            _model.Selection.Clear();

            var positions = _model.GetField().GetEmptyPosAroundOccupiedPos();

            foreach (var pos in positions)
            {
                _model.Selection.Select(pos);

                Assert.AreEqual(1, _model.Selection.Positions.Count);
            }
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return null;
        }
    }
}
