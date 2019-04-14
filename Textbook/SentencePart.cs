using System.Collections.Generic;
using Textbook.Kernel;

namespace Textbook
{
    public class SentencePart : LObject
    {
        public const int spNone = 10;
        public const int spSubject = 11;
        public const int spModalVerb = 12;
        public const int spNotionalVerb = 13;
        public const int spOtherPart = 14;

        public static SentencePart Instance { get; } = new SentencePart();

        private SentencePart()
        {
            List = new Dictionary<int, LObject>
            {
                { spNone, new LObject(spNone, "-") },
                { spSubject, new LObject(spSubject, "Подлежащее") },
                { spModalVerb, new LObject(spModalVerb, "Модальный глагол", ModalVerb.Instance.List) },
                { spNotionalVerb, new LObject(spNotionalVerb, "Смысловой глагол", NotionalVerb.Instance.List) },
                { spOtherPart, new LObject(spOtherPart, "Другие части предложения") }
            };
        }
    }
}