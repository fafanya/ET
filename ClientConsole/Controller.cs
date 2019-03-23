using System;
using System.Collections.Generic;
using System.Text;
using Workbook;
using Textbook;

namespace ClientConsole
{
    public static class Controller
    {
        public static void RunTest()
        {
            Console.WriteLine("Тест начат.");

            Test test = TestCreator.Create();
            foreach(Task task in test.TaskList)
            {
                switch (task.TaskType)
                {
                    case TaskType.MakeTence:
                        {
                            RunMakeTenceTask(task);
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
            }

            Console.WriteLine("Тест завершён.");
        }

        public static void RunMakeTenceTask(Task task)
        {
            Console.WriteLine("Задание начато.");
            Console.Write("Дано:");
            Console.WriteLine(task.NativeLangText);
            RunSelectSubTask(typeof(Tense.Time), "Выберите время:", task.TenseTime);
            RunSelectSubTask(typeof(Tense.Type), "Выберите тип:", task.TenseType);

            Console.WriteLine("Задание окончено.");
        }

        private static void RunSelectSubTask(Type t, string header, object correctValue)
        {
            Console.WriteLine(header);
            var v = (int[])(Enum.GetValues(t));
            for (int i = 1; i <= v.Length; i++)
            {
                Console.WriteLine(i.ToString() + "." + Enum.GetName(t, v[i - 1]));
            }
            int index = 0;
            bool isValid = false;
            while (!isValid)
            {
                string output = Console.ReadLine();
                if (int.TryParse(output, out index))
                {
                    index--;
                    if (index >= 0 && index < v.Length)
                    {
                        isValid = true;
                    }
                }
            }
            string correctAnswer = Enum.GetName(t, correctValue);
            string userAnsewer = Enum.GetName(t, index);

            if (correctAnswer == userAnsewer)
            {
                Console.WriteLine("Верно");
            }
            else
            {
                Console.WriteLine("Не верно");
            }
        }

        private static void RunTranslateSubTask()
        {

        }
    }
}