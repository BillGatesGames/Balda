using Zenject;

namespace Balda
{
    public interface ILocalizationManager : IInitializable
    {
        string Get(string alias);
        string GetAlphabet();
        string GetDictionaryFileName();
        void LoadLocalization();
        void SetLang(string lang);
        void UpdateLocalization();
    }
}