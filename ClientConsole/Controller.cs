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

            Test test = TestGenerator.Generate();
            foreach(Task task in test.TaskList)
            {
                switch (task.TaskType)
                {
                    case TaskType.MakeTense:
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

            /*RunSelectSubTask(typeof(VerbTense), "время", task.VerbTense);
            Console.WriteLine();

            RunSelectSubTask(typeof(VerbAspect), "вид времени", task.VerbAspect);
            Console.WriteLine();*/

            RunFormulaSubTask(task.FormulaList);
            Console.WriteLine();

            /*RunTranslateSubTask(task);
            Console.WriteLine();*/

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
            foreach(var tr in task.TranslationsList)
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

        private static void RunFormulaSubTask(List<FormulaItem[]> formulaList)
        {
            if (formulaList == null ||
                formulaList.Count == 0)
                return;

            List<string> curentFormula = new List<string>();

            Console.WriteLine("Составьте формулу:");
            bool isContinue = true;
            while (isContinue)
            {
                Console.WriteLine("Текущая формула: " + GetFormula(curentFormula));
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
                            curentFormula.Add(FormulaItem.Subject.FormulaItemTypeUID);
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
                                curentFormula.Add(ModalVerbFormulaItem.Do.ModalVerbFormulaItemUID);
                            }
                            else if (partROutput == "2")
                            {
                                curentFormula.Add(ModalVerbFormulaItem.Was.ModalVerbFormulaItemUID);
                            }
                            else if (partROutput == "3")
                            {
                                curentFormula.Add(ModalVerbFormulaItem.Were.ModalVerbFormulaItemUID);
                            }
                            else if (partROutput == "4")
                            {
                                curentFormula.Add(ModalVerbFormulaItem.Been.ModalVerbFormulaItemUID);
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
                                curentFormula.Add(NotionalVerbFormulaItem.V.NotionalVerbFormulaItemUID);
                            }
                            else if (partROutput == "2")
                            {
                                curentFormula.Add(NotionalVerbFormulaItem.Ves.NotionalVerbFormulaItemUID);
                            }
                            else if (partROutput == "3")
                            {
                                curentFormula.Add(NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemUID);
                            }
                            else if (partROutput == "4")
                            {
                                curentFormula.Add(NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemUID);
                            }
                        }
                        else if (partOutput == "4")
                        {
                            curentFormula.Add(FormulaItem.OtherPart.FormulaItemTypeUID);
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
                if (fl.Length == curentFormula.Count)
                {
                    bool rr = true;
                    for (int i = 0; i < curentFormula.Count; i++)
                    {
                        string cfData = string.Empty;
                        FormulaItem fi = fl[i];
                        if(fi is ModalVerbFormulaItem)
                        {
                            var tfi = fi as ModalVerbFormulaItem;
                            cfData = tfi.ModalVerbFormulaItemUID;
                        }
                        else if(fi is NotionalVerbFormulaItem)
                        {
                            var tfi = fi as NotionalVerbFormulaItem;
                            cfData = tfi.NotionalVerbFormulaItemUID;
                        }
                        else
                        {
                            cfData = fi.FormulaItemTypeUID;
                        }
                        if (cfData != curentFormula[i])
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