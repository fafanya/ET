using System.Collections.Generic;
using Textbook.Kernel;

namespace Textbook.Language
{
    public class Lib : LObject
    {
        public const int lTense = 1;
        public const int lAspect = 2;
        public const int lSentencePart = 3;
        public const int lTranslate = 4;

        public static Lib Instance { get; } = new Lib();

        private Lib()
        {
            List = new Dictionary<int, LObject>
            {
                { lTense, new LObject(lTense, "время", VerbTense.Instance.List) },
                { lAspect, new LObject(lAspect, "тип времени", VerbAspect.Instance.List) },
                { lSentencePart, new LObject(lSentencePart, "часть предложения", SentencePart.Instance.List) },
                { lTranslate, new LObject(lTranslate, "перевод") }
            };
        }
    }
}