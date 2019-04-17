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

            Button btnTaskInstanceRule = FindViewById<Button>(Resource.Id.btnTaskInstanceResultRule);
            btnTaskInstanceRule.Click += BtnTaskInstanceRule_Click;
            // Create your application here
            InitTaskInstace();
        }

        private void BtnTaskInstanceRule_Click(object sender, EventArgs e)
        {
            int taskInstanceId = Intent.GetIntExtra("TASK_INSTANCE_ID", 0);
            Intent intent = new Intent(this, typeof(RuleActivity));
            intent.PutExtra("TASK_INSTANCE_ID", taskInstanceId);
            StartActivityForResult(intent, taskInstanceId);
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
                    textView.Text = h + "." + taskItem.Header;
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);

                    textView = new TextView(this);
                    textView.Text = "Ваш ответ: " + "[ " + taskItem.ToString() + " ]";
                    textView.LayoutParameters = layoutParameters;
                    ll.AddView(textView);

                    foreach (TaskItem correctTaskItem in correctTaskItems.
                        Where(x => x.LangItemId == taskItem.LangItemId))
                    {
                        textView = new TextView(this);
                        textView.Text = "Верный ответ: " + "[ " + correctTaskItem.ToString() + " ]";
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