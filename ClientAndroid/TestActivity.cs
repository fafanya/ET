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
    [Activity(Label = "TestActivity")]
    public class TestActivity : Activity
    {
        private Test m_Test;
        private IEnumerator<Task> m_TaskEnumerator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_test);
            Button btnStartTest = FindViewById<Button>(Resource.Id.btnStartTest);
            btnStartTest.Click += BtnStartTest_Click;

            InitTest();
        }

        private void BtnStartTest_Click(object sender, EventArgs e)
        {
            ShowTask();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(resultCode == Result.Ok)
            {
                if (ShowTask())
                {
                    return;
                }
            }
            SetResult(resultCode);
            Finish();
        }

        private void InitTest()
        {
            m_Test = TestGenerator.Generate();
            m_TaskEnumerator = m_Test.TaskList.GetEnumerator();
        }

        private bool ShowTask()
        {
            bool isContinue = m_TaskEnumerator.MoveNext();
            if (isContinue)
            {
                Task task = m_TaskEnumerator.Current;
                Intent intent = new Intent(this, typeof(TaskActivity));
                intent.PutExtra("A_TASK_ID", task.ID);
                StartActivity(intent);
            }
            return isContinue;
        }
    }
}