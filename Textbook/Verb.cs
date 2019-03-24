using System;

namespace Textbook
{
    /// <summary>
    /// Время
    /// </summary>
    public enum VerbTense
    {
        None = 0,
        Present = 1,
        Past = 2,
        Future = 3,
        FutureInThePast = 4
    }

    /// <summary>
    /// Вид
    /// </summary>
    public enum VerbAspect
    {
        None = 0,
        Simple = 1,
        Continuous = 2,
        Perfect = 3,
        PerfectContinuous = 4
    }

    /// <summary>
    /// Тип
    /// </summary>
    public enum VerbType
    {
        None = 0,
        NotionalVerb = 1,
        AuxiliaryVerb = 2,
        ModalVerb = 3,
        LinkVerb = 4
    }
}