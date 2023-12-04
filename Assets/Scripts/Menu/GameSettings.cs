using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public static class GameSettings
    {
        private const string SIZE_KEY = "size";
        private const string LANG_KEY = "lang";
        private const string PLAYER1_TYPE = "player1_type";
        private const string PLAYER2_TYPE = "player2_type";

        public static int Size;
        public static string Lang;
        public static PlayerType Player1;
        public static PlayerType Player2;

        private static int GetInt(string key, int @default)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }

            return @default;
        }

        private static string GetStr(string key, string @default)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }

            return @default;
        }

        public static void Save()
        {
            PlayerPrefs.SetInt(SIZE_KEY, Size);
            PlayerPrefs.SetInt(PLAYER1_TYPE, (int)Player1);
            PlayerPrefs.SetInt(PLAYER2_TYPE, (int)Player2);
            PlayerPrefs.SetString(LANG_KEY, Lang);
        }

        public static void Load()
        {
            Size = GetInt(SIZE_KEY, 5);
            Player1 = (PlayerType)GetInt(PLAYER1_TYPE, (int)PlayerType.Human);
            Player2 = (PlayerType)GetInt(PLAYER2_TYPE, (int)PlayerType.AI);
            Lang = GetStr(LANG_KEY, Constants.Localization.EN);
        }
    }
}
