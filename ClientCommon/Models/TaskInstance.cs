using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class TaskInstance
    {
        [DataMember]
        public int TaskInstanceId { get; set; }
        [DataMember]
        public int SeqNo { get; set; }

        [DataMember]
        public int TestId { get; set; }
        [DataMember]
        public Test Test { get; set; }

        [DataMember]
        public int TaskId { get; set; }
        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public ICollection<TaskItem> TaskItems { get; set; }
    }
}