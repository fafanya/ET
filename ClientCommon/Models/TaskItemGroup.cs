using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class TaskItemGroup
    {
        [DataMember]
        public int TaskItemGroupId { get; set; }
        [DataMember]
        public int Name { get; set; }

        [DataMember]
        public int TaskId { get; set; }
        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public ICollection<TaskItem> TaskItems { get; set; }
        [DataMember]
        public ICollection<TaskItemInstance> TaskItemInstances { get; set; }
    }
}