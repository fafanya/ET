using System;
using System.Text;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("Введите: t - тестирование, q - выход");
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