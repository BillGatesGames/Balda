using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MessageItemData
    {
        public string Alias;
        public bool Active = true;

        public MessageItemData(string alias, bool active)
        {
            Alias = alias;
            Active = active;
        }

        public MessageItemData(bool active)
        {
            Alias = string.Empty;
            Active = active;
        }
    }

    public class MessageData
    {
        public MessageItemData Title;
        public MessageItemData Message;
        public MessageItemData LeftButton;
        public MessageItemData RightButton;

        public MessageData()
        {
            Title = new MessageItemData(false);
            Message = new MessageItemData(true);
            LeftButton = new MessageItemData(true);
            RightButton = new MessageItemData(true);
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
                        message.LeftButton = new MessageItemData("ok", true);
                        message.RightButton = new MessageItemData("reset", true);
                    }
                    break;
                case State.Completed:
                    {
                        message.Title = new MessageItemData("info", true);
                        message.Message = new MessageItemData("game_completed", true);
                        message.LeftButton = new MessageItemData("restart", true);
                        message.RightButton = new MessageItemData(false);
                    }
                    break;
            }

            switch (data.SubState)
            {
                case SubState.LetterSelection:
                    {
                        message.Message = new MessageItemData("add_new_letter", true);
                        message.LeftButton = new MessageItemData("ok", true);
                        message.RightButton = new MessageItemData("reset", true);
                    }
                    break;
                case SubState.WordSelection:
                    {
                        message.Message = new MessageItemData("select_word", true);
                        message.LeftButton = new MessageItemData("ok", true);
                        message.RightButton = new MessageItemData("reset", true);
                    }
                    break;
                case SubState.WordNotFound:
                    {
                        message.Title = new MessageItemData("info", true);
                        message.Message = new MessageItemData("word_not_found", true);
                        message.LeftButton = new MessageItemData("restart", true);
                        message.RightButton = new MessageItemData(false);
                    }
                    break;
                case SubState.WordNotExists:
                    {
                        message.Title = new MessageItemData("error", true);
                        message.Message = new MessageItemData("word_not_exists", true);
                        message.LeftButton = new MessageItemData("ok", true);
                        message.RightButton = new MessageItemData(false);
                    }
                    break;
            }

            return message;
        }
    }
}
