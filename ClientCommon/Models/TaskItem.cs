using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
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
            if (ValueInt.HasValue)
            {
                return GetNameByValueInt(LangItemId, ValueInt.Value);
            }
            return string.Empty;
        }

        public static string GetNameByValueInt(int langItemId, int valueInt)
        {
            List<LObject> lObjects = new List<LObject>();
            GetLangItemEnum(Lib.Instance.List[langItemId].Data, lObjects);
            return lObjects.First(x => x.Id == valueInt).Name;
        }

        private static void GetLangItemEnum(Dictionary<int, LObject> data, List<LObject> result)
        {
            var langItemEnum = data.Values;
            foreach(var langItem in langItemEnum)
            {
                if (langItem.Data == null)
                {
                    result.Add(langItem);
                }
                else
                {
                    GetLangItemEnum(langItem.Data, result);
                }
            }
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