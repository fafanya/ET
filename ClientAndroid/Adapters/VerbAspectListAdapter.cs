using Android.App;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using Textbook;

namespace ClientAndroid
{
    public class VerbAspectListAdapter : BaseAdapter<VerbAspect>
    {
        private VerbAspect[] m_VerbAspects;
        private Activity m_Context;

        public VerbAspectListAdapter(Activity context, VerbAspect[] verbAspects) : base()
        {
            m_Context = context;
            m_VerbAspects = verbAspects;
        }

        public override VerbAspect this[int position]
        {
            get
            {
                return m_VerbAspects[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_VerbAspects.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_VerbAspects[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? m_Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, null);
            VerbAspect verbAspect = m_VerbAspects[position];
            TextView tvText = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tvText.Text = verbAspect.Name;
            return view;
        }
    }
}