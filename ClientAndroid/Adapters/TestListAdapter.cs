using Android.App;
using Android.Views;
using Android.Widget;
using ClientCommon;

namespace ClientAndroid
{
    public class TestListAdapter : BaseAdapter<Test>
    {
        private Test[] m_Tests;
        private Activity m_Context;

        public TestListAdapter(Activity context, Test[] tests) : base()
        {
            m_Context = context;
            m_Tests = tests;
        }

        public override Test this[int position]
        {
            get
            {
                return m_Tests[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_Tests.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_Tests[position].TestId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Test test = m_Tests[position];

            View view = convertView ?? m_Context.LayoutInflater.Inflate(Resource.Layout.result_test_item, null);
            TextView tvTestDate = view.FindViewById<TextView>(Resource.Id.tvTestDate);
            TextView tvTestText = view.FindViewById<TextView>(Resource.Id.tvTestText);
            TextView tvTestHeader = view.FindViewById<TextView>(Resource.Id.tvTestHeader);
            TextView tvTestResult = view.FindViewById<TextView>(Resource.Id.tvTestResult);
            RelativeLayout rlTestDate = view.FindViewById<RelativeLayout>(Resource.Id.rlTestDate);
            RelativeLayout rlTestResult = view.FindViewById<RelativeLayout>(Resource.Id.rlTestResult);

            tvTestDate.Text = test.Date.ToString();
            tvTestText.Text = test.Header;
            tvTestHeader.Text = test.TestId.ToString();
            tvTestResult.Text = test.CorrectAnswerAmount.ToString()
                                + "/"
                                + (test.CorrectAnswerAmount + test.IncorrectAnswerAmount).ToString();

            if (test.IncorrectAnswerAmount == 0)
            {
                rlTestDate.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                rlTestResult.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
            else
            {
                rlTestDate.SetBackgroundColor(Android.Graphics.Color.LightPink);
                rlTestResult.SetBackgroundColor(Android.Graphics.Color.LightPink);
            }

            return view;
        }
    }
}