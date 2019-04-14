using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Textbook;
using Textbook.Kernel;
using Textbook.Language;

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
        public int LangItemId { get; set; }

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
        public int? UITypeId { get; set; }
        [DataMember]
        public UIType UIType { get; set; }

        public override string ToString()
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
                    result += childTaskItem.ToString();
                }
            }
            return result;
        }

        private string GetNameByValueInt()
        {
            string result = string.Empty;
            if (ValueInt.HasValue)
            {
                if (LangItemId == Lib.lTense)
                {
                    result = Lib.Instance.List[Lib.lTense].Data[ValueInt.Value].Name;
                }
                else if (LangItemId == Lib.lAspect)
                {
                    result = Lib.Instance.List[Lib.lAspect].Data[ValueInt.Value].Name;
                }
                else if (LangItemId == Lib.lSentencePart)
                {
                    result = GetSentencePartNameByValueInt(ValueInt.Value);
                }
            }
            return result;
        }

        public static string GetSentencePartNameByValueInt(int valueInt)
        {
            List<LObject> lObjects = new List<LObject>();
            lObjects.AddRange(Lib.Instance.List[Lib.lSentencePart].Data.Values);
            lObjects.AddRange(ModalVerb.Instance.List.Values);
            lObjects.AddRange(NotionalVerb.Instance.List.Values);
            return lObjects.First(x => x.Id == valueInt).Name;
        }

        public string Header
        {
            get
            {
                return Lib.Instance.List[LangItemId].Name;
            }
        }
    }
}