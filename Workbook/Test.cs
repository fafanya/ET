using System;
using System.Collections.Generic;
using System.Text;

namespace Workbook
{
    public class Test
    {
        private List<Task> m_TaskList;

        public IEnumerable<Task> TaskList
        {
            get
            {
                return m_TaskList;
            }
        }

        public Test()
        {
            m_TaskList = new List<Task>();
        }

        public void AddTask(Task task)
        {
            m_TaskList.Add(task);
            task.SeqNo = m_TaskList.Count;
        }
    }
}