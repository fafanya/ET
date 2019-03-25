using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Textbook;

namespace Workbook
{
    public class Task
    {
        public int SeqNo { get; set; }
        public TaskType TaskType { get; set; }
        public string NativeLangText { get; set; }
        public VerbTense VerbTense { get; set; }
        public VerbAspect VerbAspect { get; set; }
        public string[] TranslationsList { get; set; }
        
        public List<string[]> CompositionList { get; set; }
        public List<FormulaItem[]> FormulaList { get; set; }
    }
}