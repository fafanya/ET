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

            AddFormulaItem();

            TextView tv = FindViewById<TextView>(Resource.Id.tvNativeLangText);
            tv.Text = m_Task.NativeLangText;

            Button btnTaskOK = FindViewById<Button>(Resource.Id.btnTaskOK);
            btnTaskOK.Click += BtnTaskOK_Click;

            Button btnAddFormulaItem = FindViewById<Button>(Resource.Id.btnAddFormulaItem);
            btnAddFormulaItem.Click += BtnAddFormulaItem_Click;

            Spinner spVerbTense = FindViewById<Spinner>(Resource.Id.spVerbTense);
            spVerbTense.ItemSelected += SpVerbTense_ItemSelected;

            Spinner spVerbType = FindViewById<Spinner>(Resource.Id.spVerbType);
            spVerbType.ItemSelected += SpVerbType_ItemSelected;
        }

        private void SpVerbType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SpVerbTense_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAddFormulaItem_Click(object sender, EventArgs e)
        {
            AddFormulaItem();
        }

        private void BtnTaskOK_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void InitTask()
        {
            int taskID = Intent.GetIntExtra("A_TASK_ID", 0);
            if(taskID != 0)
            {
                m_Task = TaskDB.Instance.TaskList.First(x => x.ID == taskID);
            }
        }

        private void AddFormulaItem()
        {
            LinearLayout ll = new LinearLayout(this)
            {
                WeightSum = 2,
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };

            var lp = new TableLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent)
            {
                Weight = 1
            };

            Spinner spSentenceItem = new Spinner(this)
            {
                LayoutParameters = lp
            };
            spSentenceItem.ItemSelected += SpSentenceItem_ItemSelected;
            ll.AddView(spSentenceItem);

            Spinner spSentenceItemType = new Spinner(this)
            {
                LayoutParameters = lp
            };
            spSentenceItemType.ItemSelected += SpSentenceItemType_ItemSelected;
            ll.AddView(spSentenceItemType);

            LinearLayout llFormulaItemList = FindViewById<LinearLayout>(Resource.Id.llFormulaItemList);
            llFormulaItemList.AddView(ll);
        }

        private void SpSentenceItemType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SpSentenceItem_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}