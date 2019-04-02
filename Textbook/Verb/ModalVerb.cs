using System.Collections.Generic;

namespace Textbook
{
    public class ModalVerb : LObject
    {
        public const int mvNone = 20;
        public const int mvDo = 21;
        public const int mvWas = 22;
        public const int mvWere = 23;
        public const int mvBeen = 24;

        protected static List<LObject> m_List;
        public static IEnumerable<LObject> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<LObject>();
                    ModalVerb mv = new ModalVerb
                    {
                        Id = mvNone,
                        Name = "-"
                    };
                    m_List.Add(mv);
                    mv = new ModalVerb
                    {
                        Id = mvDo,
                        Name = "Do"
                    };
                    m_List.Add(mv);
                    mv = new ModalVerb
                    {
                        Id = mvWas,
                        Name = "Was"
                    };
                    m_List.Add(mv);
                    mv = new ModalVerb
                    {
                        Id = mvWere,
                        Name = "Were"
                    };
                    m_List.Add(mv);
                    mv = new ModalVerb
                    {
                        Id = mvBeen,
                        Name = "Been"
                    };
                    m_List.Add(mv);
                }
                return m_List;
            }
        }
    }
}