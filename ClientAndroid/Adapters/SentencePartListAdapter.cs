using Android.App;
using Android.Views;
using Android.Widget;
using Textbook;

namespace ClientAndroid
{
    public class SentencePartListAdapter : BaseAdapter<SentencePart>
    {
        private SentencePart[] m_Items;
        private Activity m_Context;

        public SentencePartListAdapter(Activity context, SentencePart[] items) : base()
        {
            m_Context = context;
            m_Items = items;
        }

        public override SentencePart this[int position]
        {
            get
            {
                return m_Items[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_Items.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_Items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? m_Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, null);
            SentencePart item = m_Items[position];
            TextView tvText = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tvText.Text = item.Name;
            return view;
        }
    }
}