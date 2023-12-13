using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public interface IWordListProvider
    {
        IWordListPresenter Get(PlayerSide side);
    }

    public class WordListProvider : IWordListProvider
    {
        private IWordListPresenter _leftList;
        private IWordListPresenter _rightList;

        public WordListProvider(IWordListPresenter leftList, IWordListPresenter rightList)
        {
            _leftList = leftList;
            _rightList = rightList;
        }

        public IWordListPresenter Get(PlayerSide side)
        {
            return side == PlayerSide.Left ? _leftList : _rightList;
        }
    }
}

