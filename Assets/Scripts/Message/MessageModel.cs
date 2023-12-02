using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MessageData
    {
        public class ItemData
        {
            public string Text;
            public bool Active = true;

            public ItemData(string text, bool active)
            {
                Text = text;
                Active = active;
            }

            public ItemData(bool active)
            {
                Text = string.Empty;
                Active = active;
            }
        }

        public ItemData Message;
        public ItemData LeftButton;
        public ItemData RightButton;

        public MessageData()
        {
            Message = new ItemData(true);
            LeftButton = new ItemData(true);
            RightButton = new ItemData(true);
        }
    }

    public class MessageModel : IMessageModel
    {
        public MessageData GetMessageData(StateData data)
        {
            var message = new MessageData();

            switch (data.State)
            {
                case State.Init:
                    {
                        message.Message.Active = false;
                        message.LeftButton = new MessageData.ItemData("OK", true);
                        message.RightButton = new MessageData.ItemData("Сбросить", true);
                    }
                    break;
                case State.Completed:
                    {
                        message.Message = new MessageData.ItemData("Игра завершена", true);
                        message.LeftButton = new MessageData.ItemData("OK", true);
                        message.RightButton = new MessageData.ItemData(false);
                    }
                    break;
            }

            switch (data.SubState)
            {
                case SubState.LetterSelection:
                    {
                        message.Message = new MessageData.ItemData("Добавьте новую букву, чтобы она образовывала новое слово", true);
                        message.LeftButton = new MessageData.ItemData("OK", true);
                        message.RightButton = new MessageData.ItemData("Сбросить", true);
                    }
                    break;
                case SubState.WordSelection:
                    {
                        message.Message = new MessageData.ItemData("Поочередно выберите каждую букву нового слова", true);
                        message.LeftButton = new MessageData.ItemData("OK", true);
                        message.RightButton = new MessageData.ItemData("Сбросить", true);
                    }
                    break;
            }

            return message;
        }
    }
}
