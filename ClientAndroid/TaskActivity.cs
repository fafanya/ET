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
using Textbook.Language;
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

            InitTaskInstance();
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
            SaveTaskInstance();
            SetResult(Result.Ok, Intent);
            Finish();
        }

        private void InitTaskInstance()
        {
            int taskInstanceId = Intent.GetIntExtra("TASK_INSTANCE_ID", 0);
            if (taskInstanceId != 0)
            {
                m_TaskInstance = DBController.Instance.GetTaskInstance(taskInstanceId);
            }
        }

        private void SaveTaskInstance()
        {
            Spinner spVerbTense = FindViewById<Spinner>(Resource.Id.spVerbTense);
            int valueInt = Convert.ToInt32(spVerbTense.SelectedItemId);
            m_TaskInstance.AddAnswer(Lib.lTense, valueInt: valueInt);

            Spinner spVerbAspect = FindViewById<Spinner>(Resource.Id.spVerbAspect);
            valueInt = Convert.ToInt32(spVerbAspect.SelectedItemId);
            m_TaskInstance.AddAnswer(Lib.lAspect, valueInt: valueInt);

            List<int> valuesInt = new List<int>();
            LinearLayout llFormulaItemList = FindViewById<LinearLayout>(Resource.Id.llFormulaItemList);
            for (int i = 0; i < llFormulaItemList.ChildCount; i++)
            {
                LinearLayout ll = llFormulaItemList.GetChildAt(i) as LinearLayout;
                Spinner sp = ll.GetChildAt(ll.ChildCount - 1) as Spinner;
                valuesInt.Add(Convert.ToInt32(sp.SelectedItemId));
            }
            m_TaskInstance.AddAnswer(Lib.lSentencePart, valuesInt: valuesInt.ToArray());

            List<string> valuesString = new List<string>();
            EditText etTranslation = FindViewById<EditText>(Resource.Id.etTranslation);
            valuesString.Add(etTranslation.Text);
            m_TaskInstance.AddAnswer(Lib.lTranslate, valuesString: valuesString.ToArray());

            DBController.Instance.SaveTaskInstance(m_TaskInstance);
            Intent.PutExtra("IS_CORRECT_TASK_INSTANCE", m_TaskInstance.IncorrectAnswerAmount == 0);
        }

        private void InitVerbTense()
        {
            Spinner spVerbTense = FindViewById<Spinner>(Resource.Id.spVerbTense);
            spVerbTense.ItemSelected += SpVerbTense_ItemSelected;

            var adapter = new LObjectListAdapter(this, VerbTense.Instance.List.Values.ToArray());
            spVerbTense.Adapter = adapter;
        }

        private void InitVerbAspect()
        {
            Spinner spVerbAspect = FindViewById<Spinner>(Resource.Id.spVerbAspect);
            spVerbAspect.ItemSelected += SpVerbAspect_ItemSelected;

            var adapter = new LObjectListAdapter(this, VerbAspect.Instance.List.Values.ToArray());
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
            var adapter = new LObjectListAdapter(this, SentencePart.Instance.List.Values.ToArray());
            spSentenceItem.Adapter = adapter;
            spSentenceItem.ItemSelected += SpSentenceItem_ItemSelected;
            ll.AddView(spSentenceItem);

            LinearLayout llFormulaItemList = FindViewById<LinearLayout>(Resource.Id.llFormulaItemList);
            llFormulaItemList.AddView(ll);
        }

        private void SpSentenceItem_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            LinearLayout ll = (sender as Spinner).Parent as LinearLayout;
            if (e.Id == SentencePart.spModalVerb ||
                e.Id == SentencePart.spNotionalVerb)
            {
                if (ll.ChildCount == 1)
                {
                    var lp = new TableLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent)
                    {
                        Weight = 1
                    };
                    Spinner spSentenceItemType = new Spinner(this)
                    {
                        LayoutParameters = lp
                    };
                    ll.AddView(spSentenceItemType);
                }

                BaseAdapter adapter;
                Spinner sp = ll.GetChildAt(1) as Spinner;
                if (e.Id == SentencePart.spModalVerb)
                {
                    adapter = new LObjectListAdapter(this, ModalVerb.Instance.List.Values.ToArray());
                }
                else
                {
                    adapter = new LObjectListAdapter(this, NotionalVerb.Instance.List.Values.ToArray());
                }
                sp.Adapter = adapter;
            }
            else
            {
                if (ll.ChildCount == 2)
                {
                    ll.RemoveViewAt(1);
                }
            }
        }
    }
}