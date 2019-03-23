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
            Console.WriteLine();
            Console.WriteLine("*******   Тест начат   *******");
            Console.WriteLine();

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

            Console.WriteLine();
            Console.WriteLine("*******   Тест завершён   *******");
        }

        public static void RunMakeTenceTask(Task task)
        {
            Console.WriteLine("-------   Задание " + task.SeqNo + "   -------");
            Console.WriteLine();

            Console.WriteLine("Дано предложение:");
            Console.WriteLine(task.NativeLangText);
            Console.WriteLine();

            RunSelectSubTask(typeof(Tense.Time), "время", task.TenseTime);
            Console.WriteLine();

            RunSelectSubTask(typeof(Tense.Type), "тип времени", task.TenseType);
            Console.WriteLine();

            RunTranslateSubTask(task);
            Console.WriteLine();

            Console.WriteLine("-------   Задание окончено   -------");
            Console.WriteLine("------------------------------------");
        }

        private static void RunSelectSubTask(Type t, string header, object correctValue)
        {
            Console.WriteLine("Выберите "+ header + ":");
            var v = (int[])(Enum.GetValues(t));
            for (int i = 1; i <= v.Length; i++)
            {
                Console.WriteLine(i.ToString() + "." + Enum.GetName(t, v[i - 1]));
            }
            int index = 0;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Номер ответа: ");
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

            bool isCorrect = false;
            if (correctAnswer == userAnsewer)
            {
                isCorrect = true;
            }

            ShowResult(isCorrect);
        }

        private static void RunTranslateSubTask(Task task)
        {
            Console.WriteLine("Переведите предложение:");

            string userAnswer = Console.ReadLine();
            string tempUserAnswer = userAnswer.Replace(".", "");
            tempUserAnswer = tempUserAnswer.Replace(" ", "");
            tempUserAnswer = tempUserAnswer.Replace(",", "");

            bool isCorrect = false;
            foreach(var tr in task.Translations)
            {
                string tempTr = tr.Replace(".", "");
                tempTr = tempTr.Replace(" ", "");
                tempTr = tempTr.Replace(",", "");

                if(tempUserAnswer == tempTr)
                {
                    isCorrect = true;
                    break;
                }
            }

            ShowResult(isCorrect);
        }

        private static void ShowResult(bool isCorrect)
        {
            Console.Write("Результат: ");
            if (isCorrect)
            {
                Console.WriteLine("Верно");
            }
            else
            {
                Console.WriteLine("Не верно");
            }
        }
    }
}