using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.EntityFrameworkCore;

using ClientCommon;
using Textbook;

namespace ClientAndroid
{
    public class DBManager
    {
        public static DBManager Instance { get; } = new DBManager();

        private DBManager()
        {
        }

        public async void RefreshDB(Context applicationContext)
        {
            ContextWrapper cw = new ContextWrapper(applicationContext);
            var dbFolder = cw.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);

            var fileName = "db.db";
            var dbFullPath = Path.Combine(dbFolder.AbsolutePath, fileName);
            try
            {
                using (var db = new ClientDBContext(dbFullPath))
                {
                    await db.Database.MigrateAsync();
                    FillDB();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void FillDB()
        {
            using (var db = new ClientDBContext())
            {
                TaskType taskType = new TaskType
                {
                    TaskTypeId = TaskType.ttChooseSentenceVerbTense,
                    Name = "Выберите время глагола в предложении"
                };
                db.Add(taskType);

                TaskItemType taskItemType = new TaskItemType
                {
                    TaskItemTypeId = TaskItemType.itChooseTense,
                    Name = "Выберите время"
                };
                db.Add(taskItemType);

                taskItemType = new TaskItemType
                {
                    TaskItemTypeId = TaskItemType.itChooseAspect,
                    Name = "Выберите тип времени"
                };
                db.Add(taskItemType);

                taskItemType = new TaskItemType
                {
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    Name = "Составьте формулу"
                };
                db.Add(taskItemType);

                taskItemType = new TaskItemType
                {
                    TaskItemTypeId = TaskItemType.itTranslate,
                    Name = "Переведите"
                };
                db.Add(taskItemType);

                Task task = new Task
                {
                    SeqNo = 1,
                    Text = "Мой брат хорошо играет в футбол.",
                    TaskTypeId = TaskType.ttChooseSentenceVerbTense
                };
                db.Add(task);

                TaskItem taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseTense,
                    ValueInt = (int)VerbTense.Present,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseAspect,
                    ValueInt = (int)VerbAspect.Simple,
                    SeqNo = 1
                };
                db.Add(taskItem);

                TaskItem parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    SeqNo = 1
                };
                db.Add(parentTaskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.OtherPart.FormulaItemTypeID,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.Subject.FormulaItemTypeID,
                    SeqNo = 2
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemID,
                    SeqNo = 3
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.OtherPart.FormulaItemTypeID,
                    SeqNo = 4
                };
                db.Add(taskItem);

                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    SeqNo = 2
                };
                db.Add(parentTaskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.Subject.FormulaItemTypeID,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = NotionalVerbFormulaItem.Vs.NotionalVerbFormulaItemID,
                    SeqNo = 2
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.OtherPart.FormulaItemTypeID,
                    SeqNo = 3
                };
                db.Add(taskItem);

                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    SeqNo = 1
                };
                db.Add(parentTaskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    ValueString = "My brother plays football well",
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    ValueString = "My brother plays good football",
                    SeqNo = 2
                };
                db.Add(taskItem);

                task = new Task
                {
                    SeqNo = 2,
                    Text = "Вчера в 9 вечера я выполнял домашнее задание.",
                    TaskTypeId = TaskType.ttChooseSentenceVerbTense
                };
                db.Add(task);

                taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseTense,
                    ValueInt = (int)VerbTense.Past,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseAspect,
                    ValueInt = (int)VerbAspect.Continuous,
                    SeqNo = 1
                };
                db.Add(taskItem);

                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    SeqNo = 1
                };
                db.Add(parentTaskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.OtherPart.FormulaItemTypeID,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.Subject.FormulaItemTypeID,
                    SeqNo = 2
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = ModalVerbFormulaItem.Was.ModalVerbFormulaItemID,
                    SeqNo = 3
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = NotionalVerbFormulaItem.Ving.NotionalVerbFormulaItemID,
                    SeqNo = 4
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = FormulaItem.OtherPart.FormulaItemTypeID,
                    SeqNo = 5
                };
                db.Add(taskItem);

                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    SeqNo = 1
                };
                db.Add(parentTaskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    ValueString = "Yesterday at 9 pm I was doing my homework.",
                    SeqNo = 1
                };
                db.Add(taskItem);
            }
        }

        internal void SaveTestResults(Test tr)
        {
            try
            {
                using (var db = new ClientDBContext())
                {
                    db.Add(tr);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}