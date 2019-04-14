using System.Collections.Generic;
using Textbook.Kernel;

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

        public static VerbAspect Instance { get; } = new VerbAspect();

        private VerbAspect()
        {
            List = new Dictionary<int, LObject>
            {
                { vaNone, new LObject(vaNone, "Не выбрано") },
                { vaSimple, new LObject(vaSimple, "Simple") },
                { vaContinuous, new LObject(vaContinuous, "Continuous") },
                { vaPerfect, new LObject(vaPerfect, "Perfect") },
                { vaPerfectContinuous, new LObject(vaPerfectContinuous, "Perfect Continuous") }
            };
        }
    }
}