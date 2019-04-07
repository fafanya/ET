using Android.App;
using Android.Views;
using Android.Widget;
using Textbook;

namespace ClientAndroid
{
    public class NotionalVerbListAdapter : BaseAdapter<NotionalVerb>
    {
        private NotionalVerb[] m_Items;
        private Activity m_Context;

        public NotionalVerbListAdapter(Activity context, NotionalVerb[] items) : base()
        {
            m_Context = context;
            m_Items = items;
        }

        public override NotionalVerb this[int position]
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
            NotionalVerb item = m_Items[position];
            TextView tvText = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tvText.Text = item.Name;
            return view;
        }
    }
}