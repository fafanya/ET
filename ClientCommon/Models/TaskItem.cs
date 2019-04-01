using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? ValueInt { get; set; }
        [DataMember]
        public string ValueString { get; set; }

        [DataMember]
        public int? TaskId { get; set; }
        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public int? TaskInstanceId { get; set; }
        [DataMember]
        public TaskInstance TaskInstance { get; set; }

        [DataMember]
        public int? ParentId { get; set; }
        [DataMember]
        [ForeignKey("ParentId")]
        public TaskItem Parent { get; set; }

        [DataMember]
        public int TaskItemTypeId { get; set; }
        [DataMember]
        public TaskItemType TaskItemType { get; set; }
    }
}