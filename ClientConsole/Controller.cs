using System;
using System.Text;
using System.Linq;
using System.Collections;
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

            TaskItem taskItem = ti.Task.TaskItems.First(x => x.TaskItemTypeId == TaskItemType.itChooseTense);
            int answer = RunSelectSubTask(typeof(VerbTense), "время", taskItem.ValueInt);
            TaskItem parentTaskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itChooseTense,
                ValueInt = answer
            };
            ti.TaskItems.Add(parentTaskItem);
            Console.WriteLine();

            taskItem = ti.Task.TaskItems.First(x => x.TaskItemTypeId == TaskItemType.itChooseAspect);
            answer = RunSelectSubTask(typeof(VerbAspect), "вид времени", taskItem.ValueInt);
            parentTaskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itChooseAspect,
                ValueInt = answer
            };
            ti.TaskItems.Add(parentTaskItem);
            Console.WriteLine();

            RunFormulaSubTask(ti);
            Console.WriteLine();

            RunTranslateSubTask(ti);
            Console.WriteLine();

            Console.WriteLine("-------   Задание окончено   -------");
            Console.WriteLine("------------------------------------");
        }

        private static int RunSelectSubTask(Type t, string header, object correctValue)
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
            return index;
        }

        private static void RunTranslateSubTask(TaskInstance taskInstance)
        {
            Console.WriteLine("Переведите предложение:");

            string userAnswer = Console.ReadLine();
            string tempUserAnswer = userAnswer.Replace(".", "");
            tempUserAnswer = tempUserAnswer.Replace(" ", "");
            tempUserAnswer = tempUserAnswer.Replace(",", "");

            bool isCorrect = false;
            foreach (var ti in taskInstance.Task.TaskItems.Where(x => x.TaskItemTypeId == TaskItemType.itTranslate))
            {
                foreach (var tiChild in ti.Children)
                {
                    string translation = tiChild.ValueString.Replace(".", "");
                    translation = translation.Replace(" ", "");
                    translation = translation.Replace(",", "");

                    if (tempUserAnswer == translation)
                    {
                        isCorrect = true;
                        break;
                    }
                }
            }

            ShowResult(isCorrect);

            TaskItem parentTaskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itTranslate,
                Children = new List<TaskItem>()
            };
            taskInstance.TaskItems.Add(parentTaskItem);

            TaskItem taskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itTranslate,
                ValueString = userAnswer
            };
            parentTaskItem.Children.Add(taskItem);
        }

        private static void RunFormulaSubTask(TaskInstance taskInstance)
        {
            IEnumerable<TaskItem> formulaList = taskInstance.Task.TaskItems.
                Where(x => x.TaskItemTypeId == TaskItemType.itMakeFormula);
            if (formulaList == null ||
                formulaList.Count() == 0)
                return;

            List<string> curentFormulaUID = new List<string>();
            List<int> curentFormulaID = new List<int>();

            Console.WriteLine("Составьте формулу:");
            bool isContinue = true;
            while (isContinue)
            {
                Console.WriteLine("Текущая формула: " + GetFormula(curentFormulaUID));
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
                        Console.WriteLine("1." + FormulaItem.Subject.FormulaItemTypeUID);
                        Console.WriteLine("2." + FormulaItem.ModalVerb.FormulaItemTypeUID);
                        Console.WriteLine("3." + FormulaItem.NotionalVerb.FormulaItemTypeUID);
                        Console.WriteLine("4." + FormulaItem.OtherPart.FormulaItemTypeUID);

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
                            curentFormulaUID.Add(FormulaItem.Subject.FormulaItemTypeUID);
                            curentFormulaID.Add(FormulaItem.Subject.FormulaItemTypeID);
                        }
                        else if (partOutput == "2")
                        {
                            Console.WriteLine("Выберите модальный глагол:");
                            Console.WriteLine("1." + ModalVerbFormulaItem.Do.ModalVerbFormulaItemUID);
                            Console.WriteLine("2." + ModalVerbFormulaItem.Was.ModalVerbFormulaItemUID);
                            Console.WriteLine("3." + ModalVerbFormulaItem.Were.ModalVerbFormulaItemUID);
                            Console.WriteLine("4." + ModalVerbFormulaItem.Been.ModalVerbFormulaItemUID);

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
                                curentFormulaUID.Add(ModalVerbFormulaItem.Do.ModalVerbFormulaItemUID);
                                curentFormulaID.Add(ModalVerbFormulaItem.Do.ModalVerbFormulaItemID);
                            }
                            else if (partROutput == "2")
                            {
                                curentFormulaUID.Add(ModalVerbFormulaItem.Was.ModalVerbFormulaItemUID);
                                curentFormulaID.Add(ModalVerbFormulaItem.Was.ModalVerbFormulaItemID);
                            }
                            else if (partROutput == "3")
                            {
                                curentFormulaUID.Add(ModalVerbFormulaItem.Were.ModalVerbFormulaItemUID);
                                curentFormulaID.Add(ModalVerbFormulaItem.Were.ModalVerbFormulaItemID);
                            }
                            else if (partROutput == "4")
                            {
                                curentFormulaUID.Add(ModalVerbFormulaItem.Been.ModalVerbFormulaItemUID);
                                curentFormulaID.Add(ModalVerbFormulaItem.Been.ModalVerbFormulaItemID);
                            }
                        }
                        else if (partOutput == "3")
                        {
                            Console.WriteLine("Выберите вид смыслового глагола:");
                            Console.WriteLine("1." + NotionalVerbFormulaItem.V.NotionalVerbFormulaItemUID);
                            Console.WriteLine("2." + NotionalVerbFormulaItem.Ves.NotionalVerbFormulaItemUID);
                            Console.WriteLine("3." + NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemUID);
                            Console.WriteLine("4." + NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemUID);

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
                                curentFormulaUID.Add(NotionalVerbFormulaItem.V.NotionalVerbFormulaItemUID);
                                curentFormulaID.Add(NotionalVerbFormulaItem.V.NotionalVerbFormulaItemID);
                            }
                            else if (partROutput == "2")
                            {
                                curentFormulaUID.Add(NotionalVerbFormulaItem.Ves.NotionalVerbFormulaItemUID);
                                curentFormulaID.Add(NotionalVerbFormulaItem.Ves.NotionalVerbFormulaItemID);
                            }
                            else if (partROutput == "3")
                            {
                                curentFormulaUID.Add(NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemUID);
                                curentFormulaID.Add(NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemID);
                            }
                            else if (partROutput == "4")
                            {
                                curentFormulaUID.Add(NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemUID);
                                curentFormulaID.Add(NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemID);
                            }
                        }
                        else if (partOutput == "4")
                        {
                            curentFormulaUID.Add(FormulaItem.OtherPart.FormulaItemTypeUID);
                            curentFormulaID.Add(FormulaItem.OtherPart.FormulaItemTypeID);
                        }
                    }
                    else if(output == "2")
                    {
                        isContinue = false;
                    }
                }
            }



            bool isCorrect = false;
            foreach(var fl in formulaList)
            {
                if (fl.Children.Count == curentFormulaUID.Count)
                {
                    bool rr = true;
                    for (int i = 0; i < curentFormulaUID.Count; i++)
                    {
                        string cfData = string.Empty;
                        TaskItem fi = fl.Children.First(x => x.SeqNo-1 == i);
                        if (fi.ValueInt != curentFormulaID[i])
                        {
                            rr = false; ;
                            break;
                        }
                    }
                    if (rr)
                    {
                        isCorrect = true;
                        break;
                    }
                }
            }

            ShowResult(isCorrect);

            TaskItem parentTaskItem = new TaskItem
            {
                TaskItemTypeId = TaskItemType.itMakeFormula,
                Children = new List<TaskItem>()
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
        }

        private static string GetFormula(IEnumerable<string> formulaItemList)
        {
            string result = string.Empty;
            result += "[ ";

            string formula = string.Join(" + ", formulaItemList);
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