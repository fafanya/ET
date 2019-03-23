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

            Task task = new Task();
            task.NativeLangText = "Мой брат хорошо играет в футбол.";
            task.Translations = new string[]
            {
                "My brother plays football well",
                "My brother plays good football"
            };
            task.TenseTime = Tense.Time.Present;
            task.TenseType = Tense.Type.Simple;
            task.TaskType = TaskType.MakeTence;
            test.TaskList.Add(task);

            return test;
        }
    }
}