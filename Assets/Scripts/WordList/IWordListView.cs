using System.Collections;
using System.Collections.Generic;

namespace Balda
{
    public interface IWordListView
    {
        void UpdateView(IEnumerable<string> words, string totalText);
    }
}