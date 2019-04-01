using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientCommon
{
    [DataContract]
    public class TaskItemType
    {
        [IgnoreDataMember]
        public const int itChooseTense = 1;
        [IgnoreDataMember]
        public const int itChooseAspect = 2;
        [IgnoreDataMember]
        public const int itMakeFormula = 3;
        [IgnoreDataMember]
        public const int itTranslate = 4;

        [DataMember]
        public int TaskItemTypeId { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<TaskItem> TaskItems { get; set; }
    }
}