using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace ClientCommon
{
    [DataContract]
    public class TaskInstance
    {
        [DataMember]
        public int TaskInstanceId { get; set; }
        [DataMember]
        public int SeqNo { get; set; }

        [DataMember]
        public int TestId { get; set; }
        [DataMember]
        public Test Test { get; set; }

        [DataMember]
        public int TaskId { get; set; }
        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public int CorrectAnswerAmount { get; set; }
        [DataMember]
        public int IncorrectAnswerAmount { get; set; }

        [DataMember]
        public ICollection<TaskItem> TaskItems { get; set; }

        public bool AddAnswer(int taskItemTypeId,
            int? valueInt = null,
            string valueString = null,
            int[] valuesInt = null,
            string[] valuesString = null)
        {
            TaskItem parentTaskItem = new TaskItem
            {
                LangItemId = taskItemTypeId,
                TaskInstance = this,
                Children = new List<TaskItem>()
            };
            TaskItem childTaskItem = new TaskItem
            {
                LangItemId = taskItemTypeId,
            };
            parentTaskItem.Children.Add(childTaskItem);

            if (valueInt != null)
            {
                childTaskItem.ValueInt = valueInt.Value;
            }
            else if (valueString != null)
            {
                childTaskItem.ValueString = valueString;
            }
            else if (valuesInt != null)
            {
                childTaskItem.Children = new List<TaskItem>();
                for (int i = 0; i < valuesInt.Length; i++)
                {
                    TaskItem taskItem = new TaskItem
                    {
                        LangItemId = taskItemTypeId,
                        ValueInt = valuesInt[i],
                        SeqNo = i + 1
                    };
                    childTaskItem.Children.Add(taskItem);
                }
            }
            else if (valuesString != null)
            {
                childTaskItem.Children = new List<TaskItem>();
                for (int i = 0; i < valuesString.Length; i++)
                {
                    TaskItem taskItem = new TaskItem
                    {
                        LangItemId = taskItemTypeId,
                        ValueString = valuesString[i],
                        SeqNo = i + 1
                    };
                    childTaskItem.Children.Add(taskItem);
                }
            }
            TaskItems.Add(parentTaskItem);
            bool isCorrect = CheckTaskItem(parentTaskItem);
            if (isCorrect)
            {
                CorrectAnswerAmount++;
            }
            else
            {
                IncorrectAnswerAmount++;
            }
            return isCorrect;
        }

        public bool CheckTaskItem(TaskItem taskItem)
        {
            TaskItem correctTaskItem = Task.TaskItems.FirstOrDefault(x => x.LangItemId == taskItem.LangItemId);
            if (correctTaskItem != null)
            {
                foreach (TaskItem correctVariant in correctTaskItem.Children)
                {
                    if (taskItem.Children != null)
                    {
                        TaskItem child = taskItem.Children.FirstOrDefault();
                        if (child != null)
                        {
                            if (IsCorrect(child, correctVariant))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static bool IsCorrect(TaskItem tiAnswer, TaskItem tiCorrect)
        {
            if (tiAnswer.LangItemId == tiCorrect.LangItemId &&
                tiAnswer.ValueInt == tiCorrect.ValueInt &&
                tiAnswer.ValueString == tiCorrect.ValueString)
            {
                if (tiAnswer.Children == null && tiCorrect.Children == null)
                {
                    return true;
                }
                else if (tiAnswer.Children != null && tiCorrect == null &&
                        tiAnswer.Children.Count == 0)
                {
                    return true;
                }
                else if (tiAnswer.Children == null && tiCorrect != null &&
                        tiCorrect.Children.Count == 0)
                {
                    return true;
                }
                else if (tiAnswer.Children != null && tiCorrect.Children == null &&
                        tiAnswer.Children.Count != 0)
                {
                    return false;
                }
                else if (tiAnswer.Children != null && tiCorrect != null &&
                        tiAnswer.Children.Count == tiCorrect.Children.Count)
                {
                    foreach (TaskItem tiAnswerChild in tiAnswer.Children)
                    {
                        TaskItem tiCorrectChild = tiCorrect.Children.FirstOrDefault(x => x.SeqNo == tiAnswerChild.SeqNo);
                        if (tiCorrectChild != null)
                        {
                            if (!IsCorrect(tiAnswerChild, tiCorrectChild))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}