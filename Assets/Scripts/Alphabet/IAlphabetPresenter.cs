using System;

namespace Balda
{
    public interface IAlphabetPresenter : IStateHandler
    {
        event Action<Cell> OnCellClick;
        void CellClick(Cell cell);
    }
}