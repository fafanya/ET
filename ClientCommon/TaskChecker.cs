using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ClientCommon
{
    public static class TaskChecker
    {
        public static void CheckTaskInstance(TaskInstance taskInstance)
        {

        }

        public static void CheckTaskItem(TaskItem taskItem)
        {
            IEnumerable<TaskItem> correctTaskItems = taskItem.Task.
                TaskItems.Where(x => x.TaskItemTypeId == taskItem.TaskItemTypeId);

            foreach(TaskItem correctTaskItem in correctTaskItems)
            {

            }
        }

        private static bool IsCorrect(TaskItem tiAnswer, TaskItem tiCorrect)
        {
            if (tiAnswer.TaskItemTypeId == tiCorrect.TaskItemTypeId &&
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