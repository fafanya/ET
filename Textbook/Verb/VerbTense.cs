using System.Collections.Generic;
using Textbook.Kernel;

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

        public static VerbTense Instance { get; } = new VerbTense();

        private VerbTense()
        {
            List = new Dictionary<int, LObject>
            {
                { vtNone, new LObject(vtNone, "Не выбрано") },
                { vtPresent, new LObject(vtPresent, "Present") },
                { vtPast, new LObject(vtPast, "Past") },
                { vtFuture, new LObject(vtFuture, "Future") },
                { vtFutureInThePast, new LObject(vtFutureInThePast, "Future In The Past") }
            };
        }
    }
}