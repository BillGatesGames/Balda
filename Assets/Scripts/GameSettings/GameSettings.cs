using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class GameSettings : IGameSettings
    {
        private const string SIZE_KEY = "size";
        private const string LANG_KEY = "lang";
        private const string PLAYER1_TYPE = "player1_type";
        private const string PLAYER2_TYPE = "player2_type";

        public int Size { get; set; }
        public string Lang { get; set; }
        public PlayerType Player1 { get; set; }
        public PlayerType Player2 { get; set; }

        public GameSettings()
        {
            Load();
        }

        private int GetInt(string key, int @default)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }

            return @default;
        }

        private string GetStr(string key, string @default)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }

            return @default;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(SIZE_KEY, Size);
            PlayerPrefs.SetInt(PLAYER1_TYPE, (int)Player1);
            PlayerPrefs.SetInt(PLAYER2_TYPE, (int)Player2);
            PlayerPrefs.SetString(LANG_KEY, Lang);
        }

        private void Load()
        {
            Size = GetInt(SIZE_KEY, Constants.Field.SIZE_5x5);
            Player1 = (PlayerType)GetInt(PLAYER1_TYPE, (int)PlayerType.Human);
            Player2 = (PlayerType)GetInt(PLAYER2_TYPE, (int)PlayerType.AI);
            Lang = GetStr(LANG_KEY, Constants.Localization.EN);
        }
    }
}
