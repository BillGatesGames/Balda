using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MessageData
    {
        public class ItemData
        {
            public string Alias;
            public bool Active = true;

            public ItemData(string alias, bool active)
            {
                Alias = alias;
                Active = active;
            }

            public ItemData(bool active)
            {
                Alias = string.Empty;
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
                        message.LeftButton = new MessageData.ItemData("ok", true);
                        message.RightButton = new MessageData.ItemData("reset", true);
                    }
                    break;
                case State.Completed:
                    {
                        message.Message = new MessageData.ItemData("game_completed", true);
                        message.LeftButton = new MessageData.ItemData("restart", true);
                        message.RightButton = new MessageData.ItemData(false);
                    }
                    break;
            }

            switch (data.SubState)
            {
                case SubState.LetterSelection:
                    {
                        message.Message = new MessageData.ItemData("add_new_letter", true);
                        message.LeftButton = new MessageData.ItemData("ok", true);
                        message.RightButton = new MessageData.ItemData("reset", true);
                    }
                    break;
                case SubState.WordSelection:
                    {
                        message.Message = new MessageData.ItemData("select_word", true);
                        message.LeftButton = new MessageData.ItemData("ok", true);
                        message.RightButton = new MessageData.ItemData("reset", true);
                    }
                    break;
            }

            return message;
        }
    }
}
