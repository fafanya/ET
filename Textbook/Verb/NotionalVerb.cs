using System.Collections.Generic;
using Textbook.Kernel;

namespace Textbook
{
    public class NotionalVerb : LObject
    {
        public const int nvNone = 30;
        public const int nvV = 31;
        public const int nvVing = 32;
        public const int nvVs = 33;
        public const int nvVes = 34;

        public static NotionalVerb Instance { get; } = new NotionalVerb();

        private NotionalVerb()
        {
            List = new Dictionary<int, LObject>
            {
                { nvNone, new LObject(nvNone, "-") },
                { nvV, new LObject(nvV, "V") },
                { nvVing, new LObject(nvVing, "V-ing") },
                { nvVs, new LObject(nvVs, "Vs") },
                { nvVes, new LObject(nvVes, "Ves") }
            };
        }
    }
}