using System;
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
            throw new NotImplementedException();
        }
    }
}