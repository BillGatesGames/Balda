using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public class AlphabetModel : IAlphabetModel
    {
        private List<char> _chars;

        public IReadOnlyList<char> Chars
        {
            get
            {
                return _chars;
            }
        }

        public bool IsLocked { get; set; } = false;

        public AlphabetModel()
        {
            _chars = "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞß".ToCharArray().ToList();
        }
    }
}
