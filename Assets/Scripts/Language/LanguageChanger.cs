using YG;

public class LanguageChanger
{
    public void SwitchLanguage(string language)
    {
        switch (language)
        {
            case LanguageConstants.LanguageEN:
                YandexGame.SwitchLanguage(LanguageConstants.LanguageEN);
                break;
            case LanguageConstants.LanguageTR:
                YandexGame.SwitchLanguage(LanguageConstants.LanguageTR);
                break;
            default:
                YandexGame.SwitchLanguage(LanguageConstants.LanguageRU);
                break;
        }
    }
}
