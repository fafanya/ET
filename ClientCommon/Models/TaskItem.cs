using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Textbook;

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
        [InverseProperty("Parent")]
        public ICollection<TaskItem> Children { get; set; }

        [DataMember]
        public int TaskItemTypeId { get; set; }
        [DataMember]
        public TaskItemType TaskItemType { get; set; }

        public string AsString()
        {
            string result = string.Empty;
            if (ValueInt.HasValue)
            {
                result += GetNameByValueInt();
            }
            if (!string.IsNullOrWhiteSpace(ValueString))
            {
                result += ValueString;
            }
            if (Children != null)
            {
                foreach (TaskItem childTaskItem in Children)
                {
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        result += " + ";
                    }
                    result += childTaskItem.AsString();
                }
            }
            return result;
        }

        private string GetNameByValueInt()
        {
            string result = string.Empty;
            if (ValueInt.HasValue)
            {
                if (TaskItemTypeId == TaskItemType.itChooseTense)
                {
                    result = VerbTense.List.First(x => x.Id == ValueInt).Name;
                }
                else if (TaskItemTypeId == TaskItemType.itChooseAspect)
                {
                    result = VerbAspect.List.First(x => x.Id == ValueInt).Name;
                }
                else if (TaskItemTypeId == TaskItemType.itMakeFormula)
                {
                    List<LObject> lObjects = new List<LObject>();
                    lObjects.AddRange(SentencePart.List);
                    lObjects.AddRange(ModalVerb.List);
                    lObjects.AddRange(NotionalVerb.List);
                    result = lObjects.First(x => x.Id == ValueInt).Name;
                }
            }
            return result;
        }
    }
}