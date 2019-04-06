using System;
using Android.App;
using Android.Views;
using Android.Widget;
using ClientCommon;

namespace ClientAndroid
{
    public class TaskListAdapter : BaseAdapter<Task>
    {
        private Task[] m_Tasks;
        private Activity m_Context;

        public TaskListAdapter(Activity context, Task[] tasks) : base()
        {
            m_Context = context;
            m_Tasks = tasks;
        }

        public override Task this[int position]
        {
            get
            {
                return m_Tasks[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_Tasks.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_Tasks[position].TaskId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}