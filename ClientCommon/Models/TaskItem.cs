using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class TaskItem
    {
        [DataMember]
        public int TaskItemId { get; set; }
        [DataMember]
        public int SeqNo { get; set; }
        [DataMember]
        public int ValueInt { get; set; }
        [DataMember]
        public string ValueString { get; set; }

        [DataMember]
        public int TaskItemGroupId { get; set; }
        [DataMember]
        public TaskItemGroup TaskItemGroup { get; set; }
    }
}