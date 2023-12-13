namespace Balda
{
    public class Constants
    {
        public class Scenes
        {
            public const int MENU = 0;
            public const int GAME = 1;
        }

        public class Field
        {
            public const int SIZE_5x5 = 5;
            public const int SIZE_7x7 = 7;
            public const int SIZE_9x9 = 9;
        }

        public class AI
        {
            public const float NEW_LETTER_SHOW_DURATION = 1f;
            public const float WORD_LETTER_SHOW_DURATION = 0.2f;
        }

        public class Localization
        {
            public const string RU = "RU";
            public const string EN = "EN";
            public const string FILE_NAME = "localization";
            public const string FALLBACK_LANG = "EN";
            public const string ERROR_PREFIX = "err_";
        }
    }
}
