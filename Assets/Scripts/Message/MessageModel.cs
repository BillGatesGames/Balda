using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MessageModel : IMessageModel
    {
        public string GetButtonText()
        {
            return "OK";
        }

        public string GetMessageText(SubState state)
        {
            switch (state)
            {
                case SubState.LetterSelection:
                    return "ƒобавьте новую букву, чтобы она образовывала новое слово";
                case SubState.WordSelection:
                    return "ѕоочередно выберите каждую букву нового слова";
            }

            return string.Empty;
        }
    }
}
