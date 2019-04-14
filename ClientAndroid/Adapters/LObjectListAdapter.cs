using Android.App;
using Android.Views;
using Android.Widget;
using Textbook;
using Textbook.Kernel;

namespace ClientAndroid
{
    public class LObjectListAdapter : BaseAdapter<LObject>
    {
        private LObject[] m_LObjects;
        private Activity m_Context;

        public LObjectListAdapter(Activity context, LObject[] lObjects) : base()
        {
            m_Context = context;
            m_LObjects = lObjects;
        }

        public override LObject this[int position]
        {
            get
            {
                return m_LObjects[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_LObjects.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_LObjects[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? m_Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, null);
            LObject verbTenses = m_LObjects[position];
            TextView tvText = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tvText.Text = verbTenses.Name;
            return view;
        }
    }
}