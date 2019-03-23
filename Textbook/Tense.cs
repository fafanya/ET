using System;

namespace Textbook
{
    public class Tense
    {
        public enum Time
        {
            None = 0,
            Present = 1,
            Past = 2,
            Future = 3
        }

        public enum Type
        {
            None = 0,
            Simple = 1,
            Continuous = 2,
            Perfect = 3,
            PerfectContinuous = 4
        }
    }
}
