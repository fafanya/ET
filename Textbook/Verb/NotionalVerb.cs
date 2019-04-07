using System.Collections.Generic;

namespace Textbook
{
    public class NotionalVerb : LObject
    {
        public const int nvNone = 30;
        public const int nvV = 31;
        public const int nvVing = 32;
        public const int nvVs = 33;
        public const int nvVes = 34;

        protected static List<NotionalVerb> m_List;
        public static IEnumerable<NotionalVerb> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<NotionalVerb>();
                    NotionalVerb nv = new NotionalVerb
                    {
                        Id = nvNone,
                        Name = "-"
                    };
                    m_List.Add(nv);
                    nv = new NotionalVerb
                    {
                        Id = nvV,
                        Name = "V"
                    };
                    m_List.Add(nv);
                    nv = new NotionalVerb
                    {
                        Id = nvVing,
                        Name = "V-ing"
                    };
                    m_List.Add(nv);
                    nv = new NotionalVerb
                    {
                        Id = nvVs,
                        Name = "Vs"
                    };
                    m_List.Add(nv);
                    nv = new NotionalVerb
                    {
                        Id = nvVes,
                        Name = "Ves"
                    };
                    m_List.Add(nv);
                }
                return m_List;
            }
        }
    }
}