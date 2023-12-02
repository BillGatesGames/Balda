using System.Collections.Generic;

namespace Balda
{
    public interface IAlphabetView
    {
        void Init(IAlphabetPresenter presenter);
        void UpdateView(IReadOnlyList<char> chars);
    }
}