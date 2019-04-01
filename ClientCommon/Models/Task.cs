using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class Task
    {
        [DataMember]
        public int TaskId { get; set; }
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int TaskTypeId { get; set; }
        [DataMember]
        public TaskType TaskType { get; set; }

        [DataMember]
        public ICollection<TaskInstance> TaskInstances { get; set; }
        [DataMember]
        public ICollection<TaskItem> TaskItems { get; set; }
    }
}