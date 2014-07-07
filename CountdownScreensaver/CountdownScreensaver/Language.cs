namespace CountdownScreensaver
{
    /// <summary>
    /// Interface to localized messages.
    /// </summary>
    public interface ILanguage
    {
        string GetMessage(int value);
        string Id { get; }
    }

    public class Czech : ILanguage
    {
        public string GetMessage(int value)
        {
            if (value >= 5)
            {
                return string.Format("Počítač se uzamkne za {0} vteřin", value);
            }
            if (value >= 2)
            {
                return string.Format("Počítač se uzamkne za {0} vteřiny", value);
            }
            if (value == 1)
            {
                return string.Format("Počítač se uzamkne za {0} vteřinu", value);
            }

            return string.Format("Počítač se uzamkne za {0} vteřin", value);
        }
        public string Id
        {
            get { return "CZE"; }
        }
    }

    public class English : ILanguage
    {
        public string GetMessage(int value)
        {
            return string.Format(value == 1  ? "The station will lock in {0} second" : "The station will lock in {0} seconds", value);
        }
        public string Id
        {
            get { return "ENG"; }
        }
    }
    /// <summary>
    /// Selects the best available language
    /// </summary>
    public class Translator : ILanguage
    {
        private readonly ILanguage _lang;
        public Translator(string threeLetterIsoLanguageName)
        {
            switch (threeLetterIsoLanguageName.ToUpper())
            {
                default:
                {
                    _lang = new English();
                    break;
                }
                case "CZE":
                {
                    _lang = new Czech();
                    break;
                }
            }
        }
        public string GetMessage(int value)
        {
            return _lang.GetMessage(value);
        }
        public string Id
        {
            get { return _lang.Id; }
        }
    }
}
