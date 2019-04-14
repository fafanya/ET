using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class UIType
    {
        [IgnoreDataMember]
        public const int uiSelect = 1;
        [IgnoreDataMember]
        public const int uiFormula = 2;
        [IgnoreDataMember]
        public const int uiText = 3;
        [IgnoreDataMember]
        public const int uiMixed = 4;

        [DataMember]
        public int UITypeId { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<TaskItem> TaskItems { get; set; }
    }
}