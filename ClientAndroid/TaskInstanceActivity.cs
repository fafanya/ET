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

using ClientCommon;

namespace ClientAndroid
{
    [Activity(Label = "TaskInstanceActivity")]
    public class TaskInstanceActivity : Activity
    {
        TaskInstance m_TaskInstance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_task_instance);

            // Create your application here
            InitTask();
            InitTaskInstance();
        }

        private void InitTaskInstance()
        {
            throw new NotImplementedException();
        }

        private void InitTask()
        {
            int taskInstanceId = Intent.GetIntExtra("TASK_INSTANCE_ID", 0);
            if (taskInstanceId != 0)
            {
                m_TaskInstance = DBController.Instance.GetTaskInstance(taskInstanceId);
                InitTaskHeader(m_TaskInstance.Task);
                foreach (TaskItem taskItem in m_TaskInstance.Task.TaskItems.OrderBy(x => x.SeqNo))
                {
                    if (taskItem.UITypeId == UIType.uiSelect)
                    {
                        InitTaskItemSelect(taskItem);
                    }
                    else if (taskItem.UITypeId == UIType.uiFormula)
                    {
                        InitTaskItemFormula(taskItem);
                    }
                    else if (taskItem.UITypeId == UIType.uiText)
                    {
                        InitTaskItemText(taskItem);
                    }
                }
            }
        }

        private void InitTaskHeader(Task task)
        {
            throw new NotImplementedException();
        }

        private void InitTaskItemText(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        private void InitTaskItemFormula(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        private void InitTaskItemSelect(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }
    }
}