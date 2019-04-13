using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Textbook;

namespace ClientCommon
{
    public class DBController
    {
        public static DBController Instance { get; } = new DBController();

        private DBController()
        {
        }

        public async void RefreshDB(string folderAbsolurePath)
        {
            var fileName = "db.db";
            var dbFullPath = Path.Combine(folderAbsolurePath, fileName);
            try
            {
                using (var db = new ClientDBContext(dbFullPath))
                {
                    await db.Database.MigrateAsync();
                    bool isInit = await db.TaskTypes.AnyAsync();
                    if (!isInit)
                    {
                        InitDB();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void InitDB()
        {
            using (var db = new ClientDBContext())
            {
                User user = new User
                {
                    Name = "ПУСТО"
                };
                db.Add(user);

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
                    Text = "Мой брат хорошо играет в футбол.",
                    TaskTypeId = TaskType.ttChooseSentenceVerbTense
                };
                db.Add(task);

                TaskItem taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseTense,
                    ValueInt = VerbTense.vtPresent,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseAspect,
                    ValueInt = VerbAspect.vaSimple,
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
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = SentencePart.spSubject,
                    SeqNo = 2
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = NotionalVerb.nvVs,
                    SeqNo = 3
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = SentencePart.spOtherPart,
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
                    ValueInt = SentencePart.spSubject,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = NotionalVerb.nvVs,
                    SeqNo = 2
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = SentencePart.spOtherPart,
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
                    ValueString = "My brother plays football well.",
                    SeqNo = 1
                };
                db.Add(taskItem);

                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    SeqNo = 2
                };
                db.Add(parentTaskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itTranslate,
                    ValueString = "My brother plays good football.",
                    SeqNo = 1
                };
                db.Add(taskItem);

                task = new Task
                {
                    Text = "Вчера в 9 вечера я выполнял домашнее задание.",
                    TaskTypeId = TaskType.ttChooseSentenceVerbTense
                };
                db.Add(task);

                taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseTense,
                    ValueInt = VerbTense.vtPast,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    TaskItemTypeId = TaskItemType.itChooseAspect,
                    ValueInt = VerbAspect.vaContinuous,
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
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 1
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = SentencePart.spSubject,
                    SeqNo = 2
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = ModalVerb.mvWas,
                    SeqNo = 3
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = NotionalVerb.nvVing,
                    SeqNo = 4
                };
                db.Add(taskItem);

                taskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    TaskItemTypeId = TaskItemType.itMakeFormula,
                    ValueInt = SentencePart.spOtherPart,
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
                db.SaveChanges();
            }
        }

        public Test GenerateTest()
        {
            using (var db = new ClientDBContext())
            {
                User user = db.Users.First();

                Test test = new Test
                {
                    Date = DateTime.Now,
                    Header = "Все подряд",
                    UserId = user.UserId
                };
                db.Add(test);

                IEnumerable<Task> tasks = db.Tasks;
                int i = 1;
                foreach(Task task in tasks)
                {
                    TaskInstance taskInstance = new TaskInstance
                    {
                        TestId = test.TestId,
                        TaskId = task.TaskId,
                        SeqNo = i
                    };
                    db.Add(taskInstance);
                    i++;
                }

                db.SaveChanges();
                return db.Tests.
                    Include(t1 => t1.TaskInstances).
                        ThenInclude(y1 => y1.Task).
                            ThenInclude(z1 => z1.TaskItems).
                                ThenInclude(ti1 => ti1.Children).
                    Include(t2 => t2.TaskInstances).
                        ThenInclude(y2 => y2.TaskItems).
                            ThenInclude(ti2 => ti2.Children).
                    FirstOrDefault(x => x.TestId == test.TestId);
            }
        }

        public IEnumerable<Test> GetTests()
        {
            using (var db = new ClientDBContext())
            {
                return db.Tests.ToList();
            }
        }

        public IEnumerable<TaskInstance> GetTaskInstancesByTestId(int testId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskInstances.
                    Where(x => x.TestId == testId).
                        Include(y => y.Task).
                            ThenInclude(z => z.TaskType).
                    ToList();
            }
        }

        public IEnumerable<TaskItem> GetTaskItemsByTaskInstanceId(int taskInstanceId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskItems.
                        Include(x => x.TaskItemType).
                        Include(x => x.Children).
                    Where(x => x.TaskInstanceId == taskInstanceId).
                    ToList();
            }
        }

        public IEnumerable<TaskItem> GetTaskItemsByTaskId(int taskId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskItems.
                        Include(x => x.TaskItemType).
                        Include(x => x.Children).
                    Where(x => x.TaskId == taskId).
                    ToList();
            }
        }

        public TaskInstance GetTaskInstance(int taskInstanceId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskInstances.
                    Include(y1 => y1.Task).
                        ThenInclude(z1 => z1.TaskItems).
                            ThenInclude(ti1 => ti1.Children).
                    Include(y2 => y2.TaskItems).
                        ThenInclude(ti2 => ti2.Children).
                    Include(y3 => y3.TaskItems).
                        ThenInclude(z3 => z3.TaskItemType).
                    First(x => x.TaskInstanceId == taskInstanceId);
            }
        }

        public void SaveTest(Test test)
        {
            using (var db = new ClientDBContext())
            {
                db.Tests.Update(test);
                db.SaveChanges();
            }
        }

        public void DeleteTest(Test test)
        {
            using (var db = new ClientDBContext())
            {
                db.Tests.Remove(test);
                db.SaveChanges();
            }
        }

        public void SaveTaskInstance(TaskInstance taskInstance)
        {
            using (var db = new ClientDBContext())
            {
                db.TaskInstances.Update(taskInstance);
                db.SaveChanges();
            }
        }
    }
}