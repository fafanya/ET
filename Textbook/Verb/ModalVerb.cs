using System.Collections.Generic;
using Textbook.Kernel;

namespace Textbook
{
    public class ModalVerb : LObject
    {
        public const int mvNone = 20;
        public const int mvDo = 21;
        public const int mvWas = 22;
        public const int mvWere = 23;
        public const int mvBeen = 24;

        public static ModalVerb Instance { get; } = new ModalVerb();

        private ModalVerb()
        {
            List = new Dictionary<int, LObject>
            {
                { mvNone, new LObject(mvNone, "-") },
                { mvDo, new LObject(mvDo, "Do") },
                { mvWas, new LObject(mvWas, "Was") },
                { mvWere, new LObject(mvWere, "Were") },
                { mvBeen, new LObject(mvBeen, "Been") }
            };
        }
    }
}