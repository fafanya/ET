using System;
using System.Collections.Generic;
using System.Text;

namespace Textbook
{
    /// <summary>
    /// Время
    /// </summary>
    public class VerbTense : LObject
    {
        public const int vtNone = 0;
        public const int vtPresent = 1;
        public const int vtPast = 2;
        public const int vtFuture = 3;
        public const int vtFutureInThePast = 4;

        protected static List<LObject> m_List;
        public static IEnumerable<LObject> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<LObject>();
                    VerbTense verbTense = new VerbTense
                    {
                        Id = vtNone,
                        Name = "Не выбрано"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbTense
                    {
                        Id = vtPresent,
                        Name = "Present"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbTense
                    {
                        Id = vtPast,
                        Name = "Past"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbTense
                    {
                        Id = vtFuture,
                        Name = "Future"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbTense
                    {
                        Id = vtFutureInThePast,
                        Name = "Future In The Past"
                    };
                    m_List.Add(verbTense);
                }
                return m_List;
            }
        }
    }
}