using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Textbook;
using Workbook;

namespace ClientAndroid
{
    [Activity(Label = "TaskActivity")]
    public class TaskActivity : Activity
    {
        private Task m_Task;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_task);

            InitTask();

            TextView tv = FindViewById<TextView>(Resource.Id.tvNativeLangText);
            tv.Text = m_Task.NativeLangText;
        }

        private void InitTask()
        {
            int taskID = Intent.GetIntExtra("A_TASK_ID", 0);
            if(taskID != 0)
            {
                m_Task = TaskDB.Instance.TaskList.First(x => x.ID == taskID);
            }
        }
    }
}