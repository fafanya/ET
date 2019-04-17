using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClientCommon
{
    public partial class DBController
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
                    bool isInit = await db.UITypes.AnyAsync();
                    if (!isInit)
                    {
                        InitDB();
                        InitTaskList();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public Test GenerateTest()
        {
            Test result = null;
            using (var db = new ClientDBContext())
            {
                Test test = new Test
                {
                    Date = DateTime.Now,
                    Header = "Все подряд"
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
                result = db.Tests.
                    Include(t1 => t1.TaskInstances).
                        ThenInclude(y1 => y1.Task).
                            ThenInclude(z1 => z1.TaskItems).
                                ThenInclude(ti1 => ti1.Children).
                                    ThenInclude(cti1 => cti1.Children).
                    Include(t2 => t2.TaskInstances).
                        ThenInclude(y2 => y2.TaskItems).
                            ThenInclude(ti2 => ti2.Children).
                    Include(t3 => t3.TaskInstances).
                        ThenInclude(y3 => y3.Task).
                            ThenInclude(z3 => z3.TaskItems).
                                ThenInclude(ti3 => ti3.UIType).
                    FirstOrDefault(x => x.TestId == test.TestId);
            }
            return result;
        }

        private void InitDB()
        {
            using (var db = new ClientDBContext())
            {
                UIType uiType;
                uiType = new UIType
                {
                    UITypeId = UIType.uiSelect,
                    Name = "Выберите"
                };
                db.Add(uiType);
                uiType = new UIType
                {
                    UITypeId = UIType.uiFormula,
                    Name = "Составьте формулу"
                };
                db.Add(uiType);
                uiType = new UIType
                {
                    UITypeId = UIType.uiText,
                    Name = "Введите текст"
                };
                db.Add(uiType);
                uiType = new UIType
                {
                    UITypeId = UIType.uiMixed,
                    Name = "Заполните"
                };
                db.Add(uiType);
                db.SaveChanges();
            }
        }

        public IEnumerable<Test> GetTests()
        {
            try
            {
                using (var db = new ClientDBContext())
                {
                    return db.Tests.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public IEnumerable<TaskInstance> GetTaskInstancesByTestId(int testId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskInstances.
                    Where(x => x.TestId == testId).
                        Include(y => y.Task).
                    ToList();
            }
        }

        public IEnumerable<TaskItem> GetTaskItemsByTaskInstanceId(int taskInstanceId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskItems.
                        Include(x => x.Children).
                            ThenInclude(y => y.UIType).
                    Where(x => x.TaskInstanceId == taskInstanceId).
                    ToList();
            }
        }

        public IEnumerable<TaskItem> GetTaskItemsByTaskId(int taskId)
        {
            using (var db = new ClientDBContext())
            {
                return db.TaskItems.
                        Include(x => x.Children).
                            ThenInclude(y => y.UIType).
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
                                ThenInclude(cti1 => cti1.Children).
                    Include(y2 => y2.TaskItems).
                        ThenInclude(ti2 => ti2.Children).
                            ThenInclude(cti2 => cti2.Children).
                    Include(y3 => y3.TaskItems).
                        ThenInclude(y => y.UIType).
                    First(x => x.TaskInstanceId == taskInstanceId);
            }
        }

        public void SaveTest(Test test)
        {
            try
            {
                using (var db = new ClientDBContext())
                {
                    db.Tests.Update(test);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteTest(Test test)
        {
            try
            {
                using (var db = new ClientDBContext())
                {
                    db.Tests.Remove(test);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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