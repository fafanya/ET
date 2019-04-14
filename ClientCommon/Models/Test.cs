using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class Test
    {
        [DataMember]
        public int TestId { get; set; }
        [DataMember]
        public string Header { get; set; }
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int CorrectAnswerAmount { get; set; }
        [DataMember]
        public int IncorrectAnswerAmount { get; set; }

        [DataMember]
        public ICollection<TaskInstance> TaskInstances { get; set; }
    }
}