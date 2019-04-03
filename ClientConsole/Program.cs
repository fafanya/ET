using System;
using System.Text;
using ClientCommon;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderAbsolurePath = Environment.CurrentDirectory;
            DBManager.Instance.RefreshDB(folderAbsolurePath);

            Console.OutputEncoding = Encoding.UTF8;
            bool toContinue = true;
            while (toContinue)
            {
                toContinue = RunMenu();
                Console.WriteLine("========================================");
            }
        }

        private static bool RunMenu()
        {
            Console.WriteLine("Введите: t - тестирование, l - список результатов, q - выход");
            string input = Console.ReadLine();

            string option = input.ToLower();
            switch (option)
            {
                case "t":
                    {
                        Controller.RunTest();
                        return true;
                    }
                case "е":
                    {
                        Controller.RunTest();
                        return true;
                    }
                case "l":
                    {
                        Controller.ShowTestList();
                        return true;
                    }
                case "q":
                    {
                        return false;
                    }
                default:
                    {
                        return true;
                    }
            }
        }
    }
}