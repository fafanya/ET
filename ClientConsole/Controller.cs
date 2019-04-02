using System;
using System.Linq;
using System.Collections.Generic;
using ClientCommon;
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

            Test test = DBManager.Instance.GenerateTest();
            foreach(TaskInstance task in test.TaskInstances)
            {
                switch (task.Task.TaskTypeId)
                {
                    case TaskType.ttChooseSentenceVerbTense:
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

            DBManager.Instance.SaveTest(test);
            Console.WriteLine();
            Console.WriteLine("*******   Тест завершён   *******");
        }

        public static void RunMakeTenceTask(TaskInstance ti)
        {
            Console.WriteLine("-------   Задание " + ti.SeqNo + "   -------");
            Console.WriteLine();

            Console.WriteLine("Дано предложение:");
            Console.WriteLine(ti.Task.Text);
            Console.WriteLine();

            RunSelectSubTask(VerbTense.List, "время", TaskItemType.itChooseTense, ti);
            Console.WriteLine();

            RunSelectSubTask(VerbAspect.List, "вид времени", TaskItemType.itChooseAspect, ti);
            Console.WriteLine();

            RunFormulaSubTask(ti);
            Console.WriteLine();

            RunTranslateSubTask(ti);
            Console.WriteLine();

            Console.WriteLine("-------   Задание окончено   -------");
            Console.WriteLine("------------------------------------");
        }

        private static void RunSelectSubTask(IEnumerable<LObject> lObjects, string header, int taskItemTypeId, TaskInstance ti)
        {
            TaskItem taskItem = ti.Task.TaskItems.First(x => x.TaskItemTypeId == taskItemTypeId);
            object correctValue = taskItem.ValueInt;

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

            TaskItem parentTaskItem = new TaskItem
            {
                TaskItemTypeId = taskItemTypeId,
                ValueInt = index,
                TaskInstance = ti
            };
            ti.TaskItems.Add(parentTaskItem);

            bool isCorrect = TaskChecker.CheckTaskItem(parentTaskItem);
            ShowResult(isCorrect);
        }

        private static void RunTranslateSubTask(TaskInstance taskInstance)
        {
            Console.WriteLine("Переведите предложение:");
            string userAnswer = Console.ReadLine();

            TaskItem parentTaskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itTranslate,
                Children = new List<TaskItem>(),
                TaskInstance = taskInstance
            };
            taskInstance.TaskItems.Add(parentTaskItem);

            TaskItem taskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itTranslate,
                ValueString = userAnswer,
                SeqNo = 1
            };
            parentTaskItem.Children.Add(taskItem);

            bool isCorrect = TaskChecker.CheckTaskItem(parentTaskItem);
            ShowResult(isCorrect);
        }

        private static void RunFormulaSubTask(TaskInstance taskInstance)
        {
            IEnumerable<TaskItem> formulaList = taskInstance.Task.TaskItems.
                Where(x => x.TaskItemTypeId == TaskItemType.itMakeFormula);
            if (formulaList == null ||
                formulaList.Count() == 0)
                return;

            List<int> curentFormulaID = new List<int>();

            Console.WriteLine("Составьте формулу:");
            bool isContinue = true;
            while (isContinue)
            {
                Console.WriteLine("Текущая формула: " + GetFormula(curentFormulaID));
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
                            curentFormulaID.Add(SentencePart.spSubject);
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
                                curentFormulaID.Add(ModalVerb.mvDo);
                            }
                            else if (partROutput == "2")
                            {
                                curentFormulaID.Add(ModalVerb.mvWas);
                            }
                            else if (partROutput == "3")
                            {
                                curentFormulaID.Add(ModalVerb.mvWere);
                            }
                            else if (partROutput == "4")
                            {
                                curentFormulaID.Add(ModalVerb.mvBeen);
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
                                curentFormulaID.Add(NotionalVerb.nvV);
                            }
                            else if (partROutput == "2")
                            {
                                curentFormulaID.Add(NotionalVerb.nvVes);
                            }
                            else if (partROutput == "3")
                            {
                                curentFormulaID.Add(NotionalVerb.nvVs);
                            }
                            else if (partROutput == "4")
                            {
                                curentFormulaID.Add(NotionalVerb.nvVing);
                            }
                        }
                        else if (partOutput == "4")
                        {
                            curentFormulaID.Add(SentencePart.spOtherPart);
                        }
                    }
                    else if(output == "2")
                    {
                        isContinue = false;
                    }
                }
            }

            TaskItem parentTaskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itMakeFormula,
                Children = new List<TaskItem>(),
                TaskInstance = taskInstance
            };
            taskInstance.TaskItems.Add(parentTaskItem);

            int j = 1;
            foreach (int cfi in curentFormulaID)
            {
                TaskItem taskItem = new TaskItem
                {
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = cfi,
                    SeqNo = j
                };
                parentTaskItem.Children.Add(taskItem);
                j++;
            }

            bool isCorrect = TaskChecker.CheckTaskItem(parentTaskItem);
            ShowResult(isCorrect);
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
        }
    }
}