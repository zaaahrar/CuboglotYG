using YG;

public static class Utils
{
    public static string GetTranslateText(string ru, string tr, string en)
    {
        string lang = YandexGame.lang;

        switch (lang)
        {
            case "tr":
                return tr;
            case "en":
                return en;
            default:
                return ru;
        }
    }
}
