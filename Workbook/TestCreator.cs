using System;
using System.Collections.Generic;
using System.Text;
using Textbook;

namespace Workbook
{
    public static class TestCreator
    {
        public static Test Create()
        {
            Test test = new Test();

            Task task = new Task
            {
                NativeLangText = "Мой брат хорошо играет в футбол.",
                Translations = new string[]
                {
                    "My brother plays football well",
                    "My brother plays good football"
                },
                TenseTime = Tense.Time.Present,
                TenseType = Tense.Type.Simple,
                TaskType = TaskType.MakeTence
            };
            test.AddTask(task);

            task = new Task
            {
                NativeLangText = "Вчера в 9 вечера я выполнял домашнее задание.",
                Translations = new string[]
                {
                    "Yesterday at 9 pm I was doing my homework.",
                },
                TenseTime = Tense.Time.Past,
                TenseType = Tense.Type.Continuous,
                TaskType = TaskType.MakeTence
            };
            test.AddTask(task);

            return test;
        }
    }
}