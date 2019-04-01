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

            Type t = typeof(VerbTense);
            List<string> verbTenseList = new List<string>();
            var v = (int[])(Enum.GetValues(t));
            for (int i = 1; i <= v.Length; i++)
            {
                verbTenseList.Add(Enum.GetName(t, v[i - 1]));
            }

            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, verbTenseList);
            spVerbTense.Adapter = adapter;
        }

        private void InitVerbAspect()
        {
            Spinner spVerbAspect = FindViewById<Spinner>(Resource.Id.spVerbAspect);
            spVerbAspect.ItemSelected += SpVerbAspect_ItemSelected;

            Type t = typeof(VerbAspect);
            List<string> verbAspectList = new List<string>();
            var v = (int[])(Enum.GetValues(t));
            for (int i = 1; i <= v.Length; i++)
            {
                verbAspectList.Add(Enum.GetName(t, v[i - 1]));
            }

            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, verbAspectList);
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

            string[] sentencePartList = new string[]
            {
                "-",
                FormulaItem.ModalVerb.FormulaItemTypeUID,
                FormulaItem.NotionalVerb.FormulaItemTypeUID,
                FormulaItem.Subject.FormulaItemTypeUID,
                FormulaItem.OtherPart.FormulaItemTypeUID
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
                sentencePartList.Add(ModalVerbFormulaItem.Been.ModalVerbFormulaItemUID);
                sentencePartList.Add(ModalVerbFormulaItem.Was.ModalVerbFormulaItemUID);
                sentencePartList.Add(ModalVerbFormulaItem.Were.ModalVerbFormulaItemUID);
                sentencePartList.Add(ModalVerbFormulaItem.Do.ModalVerbFormulaItemUID);
            }
            else if(e.Position == 2)
            {
                sentencePartList.Add(NotionalVerbFormulaItem.V.NotionalVerbFormulaItemUID);
                sentencePartList.Add(NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemUID);
                sentencePartList.Add(NotionalVerbFormulaItem.Ves.NotionalVerbFormulaItemUID);
                sentencePartList.Add(NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemUID);
            }
            else if(e.Position == 3)
            {

            }
            else if(e.Position == 4)
            {

            }
            var adapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, sentencePartList);
            sp.Adapter = adapter;
        }
    }
}