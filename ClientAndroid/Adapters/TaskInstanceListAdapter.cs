using Android.App;
using Android.Views;
using Android.Widget;
using ClientCommon;

namespace ClientAndroid
{
    public class TaskInstanceListAdapter : BaseAdapter<TaskInstance>
    {
        private TaskInstance[] m_TaskInstances;
        private Activity m_Context;

        public TaskInstanceListAdapter(Activity context, TaskInstance[] taskInstances) : base()
        {
            m_Context = context;
            m_TaskInstances = taskInstances;
        }

        public override TaskInstance this[int position]
        {
            get
            {
                return m_TaskInstances[position];
            }
        }

        public override int Count
        {
            get
            {
                return m_TaskInstances.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return m_TaskInstances[position].TaskInstanceId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TaskInstance taskInstance = m_TaskInstances[position];

            View view = convertView ?? m_Context.LayoutInflater.Inflate(Resource.Layout.result_task_item, null);
            TextView tvTaskInstanceText = view.FindViewById<TextView>(Resource.Id.tvTaskInstanceText);
            TextView tvTaskInstanceHeader = view.FindViewById<TextView>(Resource.Id.tvTaskInstanceHeader);
            TextView tvTaskInstanceResult = view.FindViewById<TextView>(Resource.Id.tvTaskInstanceResult);
            RelativeLayout rlTaskInstanceResult = view.FindViewById<RelativeLayout>(Resource.Id.rlTaskInstanceResult);

            tvTaskInstanceText.Text = taskInstance.Task.TaskType.Name;
            tvTaskInstanceHeader.Text = taskInstance.Task.Text;
            tvTaskInstanceResult.Text = taskInstance.CorrectAnswerAmount.ToString()
                                + "/"
                                + (taskInstance.CorrectAnswerAmount + taskInstance.IncorrectAnswerAmount).ToString();

            if (taskInstance.IncorrectAnswerAmount == 0)
            {
                rlTaskInstanceResult.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
            else
            {
                rlTaskInstanceResult.SetBackgroundColor(Android.Graphics.Color.LightPink);
            }

            return view;
        }
    }
}