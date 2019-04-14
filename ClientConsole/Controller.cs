using System;
using System.Linq;
using System.Collections.Generic;
using ClientCommon;
using Textbook.Kernel;
using Textbook.Language;

namespace ClientConsole
{
    public static class Controller
    {
        public static void ShowTestList()
        {
            Console.WriteLine("*******   Список тестов   *******");
            IEnumerable<Test> tests = DBController.Instance.GetTests();

            bool isContinueTest = true;
            while (isContinueTest)
            {
                int seqNoTest = 0;
                foreach (Test t in tests)
                {
                    Console.WriteLine(seqNoTest + "." + t.Header + " - " + t.Date.ToString() +
                        " [" + t.CorrectAnswerAmount + "/" +
                                    (t.CorrectAnswerAmount + t.IncorrectAnswerAmount).ToString() + "]");
                    seqNoTest++;
                }

                int? indexTest = null;
                bool isValidTestInput = false;
                while (!isValidTestInput)
                {
                    Console.Write("Введите номер теста или q для выхода:");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int temp))
                    {
                        if (temp >= 0 && temp < tests.Count())
                        {
                            isValidTestInput = true;
                            indexTest = temp;
                        }
                    }
                    else if (input == "q")
                    {
                        isContinueTest = false;
                        isValidTestInput = true;
                    }
                }

                if (indexTest.HasValue)
                {
                    bool isContinueTask = true;
                    while (isContinueTask)
                    {
                        Test test = tests.ElementAt(indexTest.Value);
                        IEnumerable<TaskInstance> taskInstances = DBController.Instance.GetTaskInstancesByTestId(test.TestId);
                        int seqNoTask = 0;
                        foreach (TaskInstance taskInstance in taskInstances)
                        {
                            Console.WriteLine(seqNoTask + "." + taskInstance.Task.Text +
                                " [" + taskInstance.CorrectAnswerAmount + "/" +
                                (taskInstance.CorrectAnswerAmount + taskInstance.IncorrectAnswerAmount).ToString() + "]");
                            seqNoTask++;
                        }

                        int? indexTask = null;
                        bool isValidTaskInput = false;
                        while (!isValidTaskInput)
                        {
                            Console.Write("Введите номер упражнения или q для выхода:");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int temp))
                            {
                                if (temp >= 0 && temp < taskInstances.Count())
                                {
                                    indexTask = temp;
                                    isValidTaskInput = true;
                                }
                            }
                            else if (input == "q")
                            {
                                isContinueTask = false;
                                isValidTaskInput = true;
                            }
                        }

                        if (indexTask.HasValue)
                        {
                            bool isContinueTaskItem = true;
                            while (isContinueTaskItem)
                            {
                                TaskInstance taskInstance = taskInstances.ElementAt(indexTask.Value);

                                TaskInstance fullTaskInstance = DBController.Instance.
                                    GetTaskInstance(taskInstance.TaskInstanceId);

                                IEnumerable<TaskItem> taskItems = fullTaskInstance.TaskItems;
                                IEnumerable<TaskItem> correctTaskItems = fullTaskInstance.Task.TaskItems;

                                int seqNoTaskItem = 0;
                                foreach (TaskItem taskItem in taskItems)
                                {
                                    Console.WriteLine("-------------------------");
                                    Console.WriteLine(fullTaskInstance.CheckTaskItem(taskItem) ? "Верно" : "Не верно");
                                    Console.WriteLine(seqNoTaskItem + "." + taskItem.Header);
                                    Console.WriteLine("Ваш ответ: " + "[ " + taskItem.ToString() + " ]");
                                    foreach (TaskItem correctTaskItem in correctTaskItems.
                                        FirstOrDefault(x => x.LangItemId == taskItem.LangItemId).Children)
                                    {
                                        Console.WriteLine("Верный ответ: " + "[ " + correctTaskItem.ToString() + " ]");
                                    }
                                    Console.WriteLine("-------------------------");
                                    seqNoTaskItem++;
                                }

                                bool isValidTaskItemInput = false;
                                while (!isValidTaskItemInput)
                                {
                                    Console.Write("Введите q для выхода:");
                                    string input = Console.ReadLine();
                                    if (input == "q")
                                    {
                                        isContinueTaskItem = false;
                                        isValidTaskItemInput = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("*********************************");
        }

        public static void RunTest()
        {
            Console.WriteLine();
            Console.WriteLine("*******   Тест начат   *******");
            Console.WriteLine();

            Test test = DBController.Instance.GenerateTest();

            int correctAnswerAmount = 0;
            int incorrectAnswerAmount = 0;
            foreach (TaskInstance task in test.TaskInstances)
            {
                if (RunTask(task))
                {
                    correctAnswerAmount++;
                }
                else
                {
                    incorrectAnswerAmount++;
                }
            }

            test.CorrectAnswerAmount = correctAnswerAmount;
            test.IncorrectAnswerAmount = incorrectAnswerAmount;
            DBController.Instance.SaveTest(test);
            Console.WriteLine();
            Console.WriteLine("*******   Тест завершён   *******");
        }
        public static bool RunTask(TaskInstance ti)
        {
            Console.WriteLine("-------   Задание " + ti.SeqNo + "   -------\n");
            Console.WriteLine("Дано предложение:\n" + ti.Task.Text + "\n");

            foreach(TaskItem taskItem in ti.Task.TaskItems)
            {
                int? valueInt = null;
                string valueString = null;
                int[] valuesInt = null;
                string[] valuesString = null;
                if (taskItem.UITypeId == UIType.uiSelect)
                {
                    valueInt = RunSubTaskSelect(taskItem);
                }
                else if (taskItem.UITypeId == UIType.uiFormula)
                {
                    valuesInt = RunSubTaskFormula(taskItem);
                }
                else if (taskItem.UITypeId == UIType.uiText)
                {
                    valueString = RunSubTaskText(taskItem);
                }
                ShowResult(ti.AddAnswer(taskItem.LangItemId, valueInt,
                                                                 valueString,
                                                                 valuesInt,
                                                                 valuesString));
            }

            Console.WriteLine("-------   Задание окончено   -------\n------------------------------------");
            return ti.IncorrectAnswerAmount == 0;
        }

        private static int RunSubTaskSelect(TaskItem taskItem)
        {
            Console.WriteLine(taskItem.UIType.Name + " " + taskItem.Header + ":");
            var lObjects = Lib.Instance.List[taskItem.LangItemId].Data;
            return RunSelect(lObjects);
        }
        private static string RunSubTaskText(TaskItem taskItem)
        {
            Console.WriteLine(taskItem.UIType.Name + " " + taskItem.Header + ":");
            return Console.ReadLine();
        }
        private static int[] RunSubTaskFormula(TaskItem taskItem)
        {
            Console.WriteLine(taskItem.UIType.Name + ":");
            List<int> valuesInt = new List<int>();
            bool isContinue = true;
            while (isContinue)
            {
                Console.WriteLine("Текущая формула: " + GetFormula(valuesInt));
                bool isValid = false;
                string output = null;
                while (!isValid)
                {
                    Console.Write("Введите [1] - добавить элемент, [2] - принять формулу:");
                    output = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        if (output == "1" || output == "2")
                        {
                            isValid = true;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(output))
                {
                    if (output == "1")
                    {
                        Console.WriteLine(taskItem.Header + ":");
                        var lObjects = Lib.Instance.List[taskItem.LangItemId].Data;
                        int valueInt = RunSelect(lObjects);
                        valuesInt.Add(valueInt);
                    }
                    else if(output == "2")
                    {
                        isContinue = false;
                    }
                }
            }
            return valuesInt.ToArray();
        }
        private static int RunSelect(Dictionary<int, LObject> lObjects)
        {
            int spSeqNo = 0;
            Dictionary<int, int> seq2id = new Dictionary<int, int>();
            foreach (var lObject in lObjects)
            {
                Console.WriteLine(spSeqNo + "." + lObject.Value.Name);
                seq2id.Add(spSeqNo, lObject.Key);
                spSeqNo++;
            }

            int index = 0;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Номер ответа: ");
                string output = Console.ReadLine();
                if (int.TryParse(output, out index))
                {
                    if (index >= 0 && index < lObjects.Count())
                    {
                        isValid = true;
                    }
                }
            }

            LObject sItem = lObjects[seq2id[index]];
            if (sItem.Data == null)
            {
                return sItem.Id;
            }
            else
            {
                return RunSelect(sItem.Data);
            }
        }

        private static string GetFormula(IEnumerable<int> formulaItemIdList)
        {
            List<string> formulaItemNameList = new List<string>();
            foreach (int formulaItemId in formulaItemIdList)
            {
                string formulaItemName = TaskItem.GetSentencePartNameByValueInt(formulaItemId);
                formulaItemNameList.Add(formulaItemName);
            }

            string result = string.Empty;
            result += "[ ";

            string formula = string.Join(" + ", formulaItemNameList);
            if (string.IsNullOrWhiteSpace(formula))
            {
                result += "Пусто";
            }
            else
            {
                result += formula;
            }

            result += " ]";
            return result;
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
            Console.WriteLine();
        }
    }
}