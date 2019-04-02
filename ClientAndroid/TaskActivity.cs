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
    [Activity(Label = "TaskActivity")]
    public class TaskActivity : Activity
    {
        private TaskInstance m_TaskInstance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_task);

            InitTask();
            AddFormulaItem();

            TextView tv = FindViewById<TextView>(Resource.Id.tvNativeLangText);
            tv.Text = m_TaskInstance.Task.Text;

            Button btnTaskOK = FindViewById<Button>(Resource.Id.btnTaskOK);
            btnTaskOK.Click += BtnTaskOK_Click;

            Button btnAddFormulaItem = FindViewById<Button>(Resource.Id.btnAddFormulaItem);
            btnAddFormulaItem.Click += BtnAddFormulaItem_Click;

            InitVerbTense();
            InitVerbAspect();
        }

        private void SpVerbAspect_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
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
            int taskInstanceId = Intent.GetIntExtra("A_TASK_ID", 0);
            if(taskInstanceId != 0)
            {
                m_TaskInstance = DBManager.Instance.GetTaskInstance(taskInstanceId);
            }
        }

        private void InitVerbTense()
        {
            Spinner spVerbTense = FindViewById<Spinner>(Resource.Id.spVerbTense);
            spVerbTense.ItemSelected += SpVerbTense_ItemSelected;

            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, VerbTense.List.Select(x => x.Name).ToArray());
            spVerbTense.Adapter = adapter;
        }

        private void InitVerbAspect()
        {
            Spinner spVerbAspect = FindViewById<Spinner>(Resource.Id.spVerbAspect);
            spVerbAspect.ItemSelected += SpVerbAspect_ItemSelected;

            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, VerbAspect.List.Select(x => x.Name).ToArray());
            spVerbAspect.Adapter = adapter;
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
            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, 
                SentencePart.List.Select(x=>x.Name).ToArray());
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
            List<string> sentencePartList;
            if (e.Position == 1)
            {
                sentencePartList = ModalVerb.List.Select(x => x.Name).ToList();
            }
            else if (e.Position == 2)
            {
                sentencePartList = NotionalVerb.List.Select(x => x.Name).ToList();
            }
            else
            {
                sentencePartList = new List<string>() { "-" };
            }
            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, sentencePartList);
            sp.Adapter = adapter;
        }
    }
}