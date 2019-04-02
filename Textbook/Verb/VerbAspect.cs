using System;
using System.Collections.Generic;
using System.Text;

namespace Textbook
{
    /// <summary>
    /// Вид времени
    /// </summary>
    public class VerbAspect : LObject
    {
        public const int vaNone = 0;
        public const int vaSimple = 1;
        public const int vaContinuous = 2;
        public const int vaPerfect = 3;
        public const int vaPerfectContinuous = 4;

        protected static List<LObject> m_List;
        public static IEnumerable<LObject> List
        {
            get
            {
                if (m_List == null)
                {
                    m_List = new List<LObject>();
                    VerbAspect verbTense = new VerbAspect
                    {
                        Id = vaNone,
                        Name = "Не выбрано"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbAspect
                    {
                        Id = vaSimple,
                        Name = "Simple"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbAspect
                    {
                        Id = vaContinuous,
                        Name = "Continuous"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbAspect
                    {
                        Id = vaPerfect,
                        Name = "Perfect"
                    };
                    m_List.Add(verbTense);
                    verbTense = new VerbAspect
                    {
                        Id = vaPerfectContinuous,
                        Name = "Perfect Continuous"
                    };
                    m_List.Add(verbTense);
                }
                return m_List;
            }
        }
    }
}