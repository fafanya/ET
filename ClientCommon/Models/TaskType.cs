using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class TaskType
    {
        [DataMember]
        public int TaskTypeId { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<Task> Tasks { get; set; }
    }
}