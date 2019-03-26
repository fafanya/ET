using System;
using System.Collections.Generic;
using System.Text;
using Textbook;
using System.Collections;
using System.Linq;

namespace Workbook
{
    public static class TestGenerator
    {
        public static Test Generate()
        {
            Test test = new Test();
            foreach(Task task in TaskDB.Instance.TaskList)
            {
                test.AddTask(task);
            }
            return test;
        }
    }
}