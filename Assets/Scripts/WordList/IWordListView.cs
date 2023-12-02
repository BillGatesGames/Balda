using System.Collections;
using System.Collections.Generic;

namespace Balda
{
    public interface IWordListView
    {
        void Init(IWordListPresenter presenter);
        void AddWord(string word);
        void UpdateView(IEnumerable<string> words, string totalText);
    }
}