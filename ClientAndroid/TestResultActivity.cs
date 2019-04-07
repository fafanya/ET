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
    [Activity(Label = "TestResultActivity")]
    public class TestResultActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_test_result);
            // Create your application here
            InitTaskInstaces();
        }

        private void InitTaskInstaces()
        {
            int testId = Intent.GetIntExtra("TEST_ID", 0);
            if (testId != 0)
            {
                IEnumerable<TaskInstance> taskInstances = DBController.Instance.GetTaskInstancesByTestId(testId);
                TaskInstanceListAdapter adapter =
                    new TaskInstanceListAdapter(this, taskInstances.ToArray());

                ListView lvTaskInstances = FindViewById<ListView>(Resource.Id.lvTaskInstances);
                lvTaskInstances.Adapter = adapter;
                lvTaskInstances.ItemClick += LvTaskInstances_ItemClick;
            }
        }

        private void LvTaskInstances_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int taskInstanceId = Convert.ToInt32(e.Id);
            Intent intent = new Intent(this, typeof(TaskInstanceResultActivity));
            intent.PutExtra("TASK_INSTANCE_ID", taskInstanceId);
            StartActivityForResult(intent, taskInstanceId);
        }
    }
}