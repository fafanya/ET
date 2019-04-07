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
using ClientCommon;

namespace ClientAndroid
{
    [Activity(Label = "TestActivity")]
    public class TestActivity : Activity
    {
        private Test m_Test;
        private IEnumerator<TaskInstance> m_TaskEnumerator;

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
                bool isCorrect = data.GetBooleanExtra("IS_CORRECT_TASK_INSTANCE", false);
                if (isCorrect)
                {
                    m_Test.CorrectAnswerAmount++;
                }
                else
                {
                    m_Test.IncorrectAnswerAmount++;
                }

                if (ShowTask())
                {
                    return;
                }
                else
                {
                    m_Test.TaskInstances = null;
                    DBController.Instance.SaveTest(m_Test);
                }
            }
            SetResult(resultCode);
            Finish();
        }

        private void InitTest()
        {
            m_Test = DBController.Instance.GenerateTest();
            m_TaskEnumerator = m_Test.TaskInstances.GetEnumerator();
        }

        private bool ShowTask()
        {
            bool isContinue = m_TaskEnumerator.MoveNext();
            if (isContinue)
            {
                TaskInstance task = m_TaskEnumerator.Current;
                Intent intent = new Intent(this, typeof(TaskActivity));
                intent.PutExtra("A_TASK_ID", task.TaskInstanceId);
                StartActivityForResult(intent, task.TaskInstanceId);
            }
            return isContinue;
        }
    }
}