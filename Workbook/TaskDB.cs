using System;
using System.Collections.Generic;
using System.Text;
using Textbook;

namespace Workbook
{
    public class TaskDB
    {
        private List<Task> m_TaskList;

        public static TaskDB Instance { get; } = new TaskDB();

        public IEnumerable<Task> TaskList
        {
            get
            {
                return m_TaskList;
            }
        }

        private TaskDB()
        {
            m_TaskList = new List<Task>();
            Task task = new Task(1)
            {
                NativeLangText = "Мой брат хорошо играет в футбол.",
                TranslationsList = new string[]
                {
                    "My brother plays football well",
                    "My brother plays good football"
                },
                VerbTense = VerbTense.Present,
                VerbAspect = VerbAspect.Simple,
                TaskType = TaskType.MakeTense,
                FormulaList = new List<FormulaItem[]>
                {
                    new FormulaItem[]
                    {
                        FormulaItem.OtherPart,
                        FormulaItem.Subject,
                        NotionalVerbFormulaItem.Vs,
                        FormulaItem.OtherPart
                    },
                    new FormulaItem[]
                    {
                        FormulaItem.Subject,
                        NotionalVerbFormulaItem.Vs,
                        FormulaItem.OtherPart
                    }
                }
            };
            m_TaskList.Add(task);
            task = new Task(2)
            {
                NativeLangText = "Вчера в 9 вечера я выполнял домашнее задание.",
                TranslationsList = new string[]
                {
                    "Yesterday at 9 pm I was doing my homework.",
                },
                VerbTense = VerbTense.Past,
                VerbAspect = VerbAspect.Continuous,
                TaskType = TaskType.MakeTense,
                FormulaList = new List<FormulaItem[]>
                {
                    new FormulaItem[]
                    {
                        FormulaItem.OtherPart,
                        FormulaItem.Subject,
                        ModalVerbFormulaItem.Was,
                        NotionalVerbFormulaItem.Ving,
                        FormulaItem.OtherPart
                    }
                }
            };
            m_TaskList.Add(task);
        }
    }
}