using System;
using System.Collections.Generic;
using System.Text;

namespace Textbook
{
    public class SentencePart : LObject
    {
        public const int spNone = 10;
        public const int spSubject = 11;
        public const int spModalVerb = 12;
        public const int spNotionalVerb = 13;
        public const int spOtherPart = 14;

        protected static List<SentencePart> m_List;
        public static IEnumerable<SentencePart> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<SentencePart>();
                    SentencePart sp = new SentencePart
                    {
                        Id = spNone,
                        Name = "-"
                    };
                    m_List.Add(sp);
                    sp = new SentencePart
                    {
                        Id = spSubject,
                        Name = "Подлежащее"
                    };
                    m_List.Add(sp);
                    sp = new SentencePart
                    {
                        Id = spModalVerb,
                        Name = "Модальный глагол"
                    };
                    m_List.Add(sp);
                    sp = new SentencePart
                    {
                        Id = spNotionalVerb,
                        Name = "Смысловой глагол"
                    };
                    m_List.Add(sp);
                    sp = new SentencePart
                    {
                        Id = spOtherPart,
                        Name = "Другие части предложения"
                    };
                    m_List.Add(sp);
                }
                return m_List;
            }
        }
    }
}