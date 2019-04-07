using System;
using System.Linq;
using System.Collections.Generic;
using ClientCommon;
using Textbook;

namespace ClientConsole
{
    public static class Controller
    {
        public static void ShowTestList()
        {
            Console.WriteLine("*******   Список тестов   *******");
            IEnumerable<Test> tests = DBController.Instance.GetTests();

            bool isContinue = true;
            while (isContinue)
            {
                int i = 0;
                foreach (Test t in tests)
                {
                    Console.WriteLine(i + "." + t.Header + " - " + t.Date.ToString() +
                        " [" + t.CorrectAnswerAmount + "/" +
                                    (t.CorrectAnswerAmount + t.IncorrectAnswerAmount).ToString() + "]");
                    i++;
                }

                int index = -1;
                bool isValid = false;
                while (!isValid)
                {
                    Console.Write("Введите номер теста или q для выхода:");
                    string output = Console.ReadLine();
                    if (int.TryParse(output, out index))
                    {
                        if (index >= 0 && index < tests.Count())
                        {
                            isValid = true;
                        }
                    }
                    else if(output == "q")
                    {
                        isContinue = false;
                        isValid = true;
                        index = -1;
                    }
                    else
                    {
                        index = -1;
                    }
                }

                if (isValid)
                {
                    if (index >= 0)
                    {
                        bool isContinueJ = true;
                        while (isContinueJ)
                        {
                            Test test = tests.ElementAt(index);
                            IEnumerable<TaskInstance> taskInstances = DBController.Instance.GetTaskInstancesByTestId(test.TestId);
                            int j = 0;
                            foreach (TaskInstance taskInstance in taskInstances)
                            {
                                Console.WriteLine(j + "." + taskInstance.Task.TaskType.Name + 
                                    " [" + taskInstance.CorrectAnswerAmount + "/" + 
                                    (taskInstance.CorrectAnswerAmount + taskInstance.IncorrectAnswerAmount).ToString() +"]");
                                j++;
                            }


                            int indexJ = -1;
                            bool isValidJ = false;
                            while (!isValidJ)
                            {
                                Console.Write("Введите номер упражнения или q для выхода:");
                                string outputJ = Console.ReadLine();
                                if (int.TryParse(outputJ, out indexJ))
                                {
                                    if (indexJ >= 0 && indexJ < taskInstances.Count())
                                    {
                                        isValidJ = true;
                                    }
                                }
                                else if (outputJ == "q")
                                {
                                    isContinueJ = false;
                                    isValidJ = true;
                                    indexJ = -1;
                                }
                                else
                                {
                                    indexJ = -1;
                                }
                            }

                            if (isValidJ)
                            {
                                if (indexJ >= 0)
                                {
                                    bool isContinueH = true;
                                    while (isContinueH)
                                    {
                                        TaskInstance taskInstance = taskInstances.ElementAt(indexJ);

                                        TaskInstance fullTaskInstance = DBController.Instance.
                                            GetTaskInstance(taskInstance.TaskInstanceId);

                                        IEnumerable<TaskItem> taskItems = fullTaskInstance.TaskItems;
                                        IEnumerable<TaskItem> correctTaskItems = fullTaskInstance.Task.TaskItems;

                                        int h = 0;
                                        foreach (TaskItem taskItem in taskItems)
                                        {
                                            Console.WriteLine("-------------------------");
                                            Console.WriteLine(fullTaskInstance.CheckTaskItem(taskItem));
                                            Console.WriteLine(h + "." + taskItem.TaskItemType.Name);
                                            Console.WriteLine("Ваш ответ: " + "[ " + PrintTaskItem(taskItem) + " ]");
                                            foreach (TaskItem correctTaskItem in correctTaskItems.
                                                Where(x=>x.TaskItemTypeId == taskItem.TaskItemTypeId))
                                            {
                                                Console.WriteLine("Верный ответ: " + "[ " + PrintTaskItem(correctTaskItem) + " ]");
                                            }
                                            Console.WriteLine("-------------------------");
                                            h++;
                                        }

                                        bool isValidH = false;
                                        while (!isValidH)
                                        {
                                            Console.Write("Введите q для выхода:");
                                            string outputH = Console.ReadLine();
                                            if (outputH == "q")
                                            {
                                                isContinueH = false;
                                                isValidH = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("*********************************");
        }

        private static string PrintTaskItem(TaskItem taskItem)
        {
            string result = string.Empty;
            if (taskItem.ValueInt.HasValue)
            {
                result += GetNameByValueInt(taskItem);
            }
            if (!string.IsNullOrWhiteSpace(taskItem.ValueString))
            {
                result += taskItem.ValueString;
            }
            if (taskItem.Children != null)
            {
                foreach (TaskItem childTaskItem in taskItem.Children)
                {
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        result += " + ";
                    }
                    result += PrintTaskItem(childTaskItem);
                }
            }

            return result;
        }

        private static string GetNameByValueInt(TaskItem taskItem)
        {
            string result = string.Empty;
            if (taskItem.ValueInt.HasValue)
            {
                if (taskItem.TaskItemTypeId == TaskItemType.itChooseTense)
                {
                    result = VerbTense.List.First(x => x.Id == taskItem.ValueInt).Name;
                }
                else if (taskItem.TaskItemTypeId == TaskItemType.itChooseAspect)
                {
                    result = VerbAspect.List.First(x => x.Id == taskItem.ValueInt).Name;
                }
                else if(taskItem.TaskItemTypeId == TaskItemType.itMakeFormula)
                {
                    List<LObject> lObjects = new List<LObject>();
                    lObjects.AddRange(SentencePart.List);
                    lObjects.AddRange(ModalVerb.List);
                    lObjects.AddRange(NotionalVerb.List);
                    result = lObjects.First(x => x.Id == taskItem.ValueInt).Name;
                }
            }
            return result;
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
                switch (task.Task.TaskTypeId)
                {
                    case TaskType.ttChooseSentenceVerbTense:
                        {
                            if (RunMakeTenceTask(task))
                            {
                                correctAnswerAmount++;
                            }
                            else
                            {
                                incorrectAnswerAmount++;
                            }
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
            }

            test.CorrectAnswerAmount = correctAnswerAmount;
            test.IncorrectAnswerAmount = incorrectAnswerAmount;
            DBController.Instance.SaveTest(test);
            Console.WriteLine();
            Console.WriteLine("*******   Тест завершён   *******");
        }

        public static bool RunMakeTenceTask(TaskInstance ti)
        {
            Console.WriteLine("-------   Задание " + ti.SeqNo + "   -------\n");
            Console.WriteLine("Дано предложение:\n" + ti.Task.Text + "\n");

            RunSelectSubTask(VerbTense.List, "время", TaskItemType.itChooseTense, ti);
            RunSelectSubTask(VerbAspect.List, "вид времени", TaskItemType.itChooseAspect, ti);
            RunFormulaSubTask(ti);
            RunTranslateSubTask(ti);

            Console.WriteLine("-------   Задание окончено   -------\n------------------------------------");
            return ti.IncorrectAnswerAmount == 0;
        }

        private static void RunSelectSubTask(IEnumerable<LObject> lObjects, string header, int taskItemTypeId, TaskInstance ti)
        {
            Console.WriteLine("Выберите "+ header + ":");
            foreach(LObject lObject in lObjects)
            {
                Console.WriteLine(lObject.Id + "." + lObject.Name);
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

            ShowResult(ti.AddAnswer(taskItemTypeId, index));
        }

        private static void RunTranslateSubTask(TaskInstance ti)
        {
            Console.WriteLine("Переведите предложение:");
            string userAnswer = Console.ReadLine();
            ShowResult(ti.AddAnswer(TaskItemType.itTranslate, valueString: userAnswer));
        }

        private static void RunFormulaSubTask(TaskInstance ti)
        {
            List<int> valuesInt = new List<int>();

            Console.WriteLine("Составьте формулу:");
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
                        Console.WriteLine("Выберите часть предложения:");
                        int spSeqNo = 0;
                        foreach(SentencePart sp in SentencePart.List)
                        {
                            Console.WriteLine(spSeqNo + "." + sp.Name);
                            spSeqNo++;
                        }

                        bool isPartValid = false;
                        string partOutput = null;
                        while (!isPartValid)
                        {
                            Console.Write("Ваш вариант:");
                            partOutput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(partOutput))
                            {
                                if (partOutput == "1" || partOutput == "2" || partOutput == "3" || partOutput == "4")
                                {
                                    isPartValid = true;
                                }
                            }
                        }

                        if (partOutput == "1")
                        {
                            valuesInt.Add(SentencePart.spSubject);
                        }
                        else if (partOutput == "2")
                        {
                            Console.WriteLine("Выберите модальный глагол:");
                            int mvSeqNo = 0;
                            foreach (ModalVerb mv in ModalVerb.List)
                            {
                                Console.WriteLine(mvSeqNo + "." + mv.Name);
                                mvSeqNo++;
                            }

                            bool isRPartValid = false;
                            string partROutput = null;
                            while (!isRPartValid)
                            {
                                Console.Write("Ваш вариант:");
                                partROutput = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(partROutput))
                                {
                                    if (partROutput == "1" || partROutput == "2" || partROutput == "3" || partROutput == "4")
                                    {
                                        isRPartValid = true;
                                    }
                                }
                            }

                            if(partROutput == "1")
                            {
                                valuesInt.Add(ModalVerb.mvDo);
                            }
                            else if (partROutput == "2")
                            {
                                valuesInt.Add(ModalVerb.mvWas);
                            }
                            else if (partROutput == "3")
                            {
                                valuesInt.Add(ModalVerb.mvWere);
                            }
                            else if (partROutput == "4")
                            {
                                valuesInt.Add(ModalVerb.mvBeen);
                            }
                        }
                        else if (partOutput == "3")
                        {
                            Console.WriteLine("Выберите вид смыслового глагола:");
                            int nvSeqNo = 0;
                            foreach (NotionalVerb mv in NotionalVerb.List)
                            {
                                Console.WriteLine(nvSeqNo + "." + mv.Name);
                                nvSeqNo++;
                            }

                            bool isRPartValid = false;
                            string partROutput = null;
                            while (!isRPartValid)
                            {
                                Console.Write("Ваш вариант:");
                                partROutput = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(partROutput))
                                {
                                    if (partROutput == "1" || partROutput == "2" || partROutput == "3" || partROutput == "4")
                                    {
                                        isRPartValid = true;
                                    }
                                }
                            }

                            if (partROutput == "1")
                            {
                                valuesInt.Add(NotionalVerb.nvV);
                            }
                            else if (partROutput == "2")
                            {
                                valuesInt.Add(NotionalVerb.nvVes);
                            }
                            else if (partROutput == "3")
                            {
                                valuesInt.Add(NotionalVerb.nvVs);
                            }
                            else if (partROutput == "4")
                            {
                                valuesInt.Add(NotionalVerb.nvVing);
                            }
                        }
                        else if (partOutput == "4")
                        {
                            valuesInt.Add(SentencePart.spOtherPart);
                        }
                    }
                    else if(output == "2")
                    {
                        isContinue = false;
                    }
                }
            }

            ShowResult(ti.AddAnswer(TaskItemType.itMakeFormula, valuesInt: valuesInt.ToArray()));
        }

        private static string GetFormula(IEnumerable<int> formulaItemIdList)
        {
            List<LObject> lObjects = new List<LObject>();
            lObjects.AddRange(SentencePart.List);
            lObjects.AddRange(ModalVerb.List);
            lObjects.AddRange(NotionalVerb.List);

            List<string> formulaItemNameList = new List<string>();
            foreach (int formulaItemId in formulaItemIdList)
            {
                string formulaItemName = lObjects.First(x => x.Id == formulaItemId).Name;
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