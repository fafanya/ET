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
    [Activity(Label = "TaskInstanceResultActivity")]
    public class TaskInstanceResultActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_task_instance_result);
            // Create your application here
            InitTaskInstace();
        }

        private void InitTaskInstace()
        {
            int taskInstanceId = Intent.GetIntExtra("TASK_INSTANCE_ID", 0);
            if (taskInstanceId != 0)
            {
                LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.llTaskInstanceResult);
                var layoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

                TaskInstance taskInstance = DBController.Instance.GetTaskInstance(taskInstanceId);
                IEnumerable<TaskItem> taskItems = taskInstance.TaskItems;
                IEnumerable<TaskItem> correctTaskItems = taskInstance.Task.TaskItems;
                int h = 0;
                foreach (TaskItem taskItem in taskItems)
                {
                    TextView textView = new TextView(this);
                    textView.Text = "-------------------------";
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);

                    textView = new TextView(this);
                    textView.Text = taskInstance.CheckTaskItem(taskItem) ? "Верно" : "Не верно";
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);

                    textView = new TextView(this);
                    textView.Text = h + "." + taskItem.TaskItemType.Name;
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);

                    textView = new TextView(this);
                    textView.Text = "Ваш ответ: " + "[ " + taskItem.AsString() + " ]";
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);

                    foreach (TaskItem correctTaskItem in correctTaskItems.
                        Where(x => x.TaskItemTypeId == taskItem.TaskItemTypeId))
                    {
                        textView = new TextView(this);
                        textView.Text = "Верный ответ: " + "[ " + correctTaskItem.AsString() + " ]";
                        textView.LayoutParameters = layoutParameters;
                        ll.AddView(textView);
                    }

                    textView = new TextView(this);
                    textView.Text = "-------------------------";
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);
                    h++;
                }
            }
        }
    }
}