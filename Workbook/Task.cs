using System;
using Textbook;

namespace Workbook
{
    public class Task
    {
        public int SeqNo { get; set; }
        public TaskType TaskType { get; set; }
        public string NativeLangText { get; set; }
        public Tense.Time TenseTime { get; set; }
        public Tense.Type TenseType { get; set; }
        public string[] Translations { get; set; }
    }
}