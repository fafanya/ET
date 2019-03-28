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
        }

        private void SpVerbTense_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void BtnAddFormulaItem_Click(object sender, EventArgs e)
        {
            AddFormulaItem();
        }

        private void BtnTaskOK_Click(object sender, EventArgs e)
        {
            SetResult(Result.Ok);
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

            string[] sentencePartList = new string[]
            {
                "-",
                FormulaItem.ModalVerb.FormulaItemTypeID,
                FormulaItem.NotionalVerb.FormulaItemTypeID,
                FormulaItem.Subject.FormulaItemTypeID,
                FormulaItem.OtherPart.FormulaItemTypeID
            };
            Spinner spSentenceItem = new Spinner(this)
            {
                LayoutParameters = lp
            };
            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, sentencePartList);
            spSentenceItem.Adapter = adapter;
            spSentenceItem.ItemSelected += SpSentenceItem_ItemSelected;
            ll.AddView(spSentenceItem);

            Spinner spSentenceItemType = new Spinner(this)
            {
                LayoutParameters = lp
            };
            adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, new string[] { "-" });
            spSentenceItemType.Adapter = adapter;
            spSentenceItemType.ItemSelected += SpSentenceItemType_ItemSelected;
            ll.AddView(spSentenceItemType);

            LinearLayout llFormulaItemList = FindViewById<LinearLayout>(Resource.Id.llFormulaItemList);
            llFormulaItemList.AddView(ll);
        }

        private void SpSentenceItemType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void SpSentenceItem_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            LinearLayout ll = (sender as Spinner).Parent as LinearLayout;
            Spinner sp = ll.GetChildAt(1) as Spinner;
            List<string> sentencePartList = new List<string>() { "-" };
            if (e.Position == 0)
            {
                
            }
            if(e.Position == 1)
            {
                sentencePartList.Add(ModalVerbFormulaItem.Been.ModalVerbFormulaItemID);
                sentencePartList.Add(ModalVerbFormulaItem.Was.ModalVerbFormulaItemID);
                sentencePartList.Add(ModalVerbFormulaItem.Were.ModalVerbFormulaItemID);
                sentencePartList.Add(ModalVerbFormulaItem.Do.ModalVerbFormulaItemID);
            }
            else if(e.Position == 2)
            {
                sentencePartList.Add(NotionalVerbFormulaItem.V.NotionalVerbFormulaItemID);
                sentencePartList.Add(NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemID);
                sentencePartList.Add(NotionalVerbFormulaItem.Ves.NotionalVerbFormulaItemID);
                sentencePartList.Add(NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemID);
            }
            else if(e.Position == 3)
            {

            }
            else if(e.Position == 4)
            {

            }
            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, sentencePartList.ToArray());
            sp.Adapter = adapter;
        }
    }
}