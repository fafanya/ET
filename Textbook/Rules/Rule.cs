using System.Collections.Generic;
using Textbook.Kernel;

namespace Textbook
{
    public class Rule : LObject
    {
        public const int rNone = 0;
        public const int rPastContinuous = 21;
        public const int rPastSimple = 22;

        public static Rule Instance { get; } = new Rule();

        private Rule()
        {
            List = new Dictionary<int, LObject>
            {
                { rNone, new LObject(rNone, "-") },
                { rPastContinuous, new LObject(rPastContinuous, "Past Continuous") },
                { rPastSimple, new LObject(rPastSimple, "Past Simple") }
            };
        }
    }
}