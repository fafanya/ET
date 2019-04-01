using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class TaskType
    {
        [IgnoreDataMember]
        public const int ttChooseSentenceVerbTense = 1;

        [DataMember]
        public int TaskTypeId { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<Task> Tasks { get; set; }
    }
}