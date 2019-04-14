using System.Collections.Generic;

namespace Textbook.Kernel
{
    /// <summary>
    /// Объект языка
    /// </summary>
    public class LObject
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public Dictionary<int, LObject> Data { get; protected set; }
        public Dictionary<int, LObject> List { get; protected set; }

        public LObject(int id, string name, Dictionary<int, LObject> data = null)
        {
            Id = id;
            Name = name;
            Data = data;
        }

        public LObject()
        {

        }
    }
}