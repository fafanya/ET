using Android.App;
using Android.Views;
using Android.Widget;
using Textbook;

namespace ClientAndroid
{
    public class VerbTenseListAdapter : BaseAdapter<VerbTense>
    {
        private VerbTense[] m_VerbTenses;
        private Activity m_Context;

        public VerbTenseListAdapter(Activity context, VerbTense[] verbTenses) : base()
        {
            m_Context = context;
            m_VerbTenses = verbTenses;
        }

        public override VerbTense this[int position]
        {
            get
            {
                return m_VerbTenses[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_VerbTenses.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_VerbTenses[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? m_Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, null);
            VerbTense verbTenses = m_VerbTenses[position];
            TextView tvText = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tvText.Text = verbTenses.Name;
            return view;
        }
    }
}