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
                    return "�������� ����� �����, ����� ��� ������������ ����� �����";
                case SubState.WordSelection:
                    return "���������� �������� ������ ����� ������ �����";
            }

            return string.Empty;
        }
    }
}
