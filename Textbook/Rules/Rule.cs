using System;
using System.Collections.Generic;
using System.Text;

namespace Textbook
{
    public class Rule : LObject
    {
        public const int rNone = 0;
        public const int rPastContinuous = 21;
        public const int rPastSimple = 22;

        protected static List<Rule> m_List;
        public static IEnumerable<Rule> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<Rule>();
                    Rule r = new Rule
                    {
                        Id = rNone,
                        Name = "-"
                    };
                    m_List.Add(r);
                    r = new Rule
                    {
                        Id = rPastContinuous,
                        Name = "Past Continuoas"
                    };
                    m_List.Add(r);
                    r = new Rule
                    {
                        Id = rPastSimple,
                        Name = "Past Simple"
                    };
                }
                return m_List;
            }
        }
    }
}