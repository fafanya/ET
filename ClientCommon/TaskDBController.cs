using System;
using System.Collections.Generic;
using System.Text;
using Textbook;
using Textbook.Language;

namespace ClientCommon
{
    public partial class DBController
    {
        private void InitTaskList()
        {
            AddTask_1();
            AddTask_2();
        }

        private void AddTask_1()
        {
            using (var db = new ClientDBContext())
            {
                Task task = new Task
                {
                    Text = "Мой брат хорошо играет в футбол."
                };
                db.Add(task);

                TaskItem parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lTense,
                    SeqNo = 1,
                    UITypeId = UIType.uiSelect
                };
                db.Add(parentTaskItem);
                TaskItem childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lTense,
                    ValueInt = VerbTense.vtPresent,
                    UITypeId = UIType.uiSelect
                };
                db.Add(childTaskItem);


                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lAspect,
                    SeqNo = 2,
                    UITypeId = UIType.uiSelect
                };
                db.Add(parentTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lAspect,
                    ValueInt = VerbAspect.vaSimple,
                    UITypeId = UIType.uiSelect
                };
                db.Add(childTaskItem);


                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lSentencePart,
                    SeqNo = 3,
                    UITypeId = UIType.uiFormula
                };
                db.Add(parentTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childTaskItem);
                TaskItem childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 1,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);
                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spSubject,
                    SeqNo = 2,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);
                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = NotionalVerb.nvVs,
                    SeqNo = 3,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);
                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 4,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);

                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childTaskItem);
                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spSubject,
                    SeqNo = 1,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);
                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = NotionalVerb.nvVs,
                    SeqNo = 2,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);
                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 3,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);


                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lTranslate,
                    SeqNo = 4,
                    UITypeId = UIType.uiText
                };
                db.Add(parentTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lTranslate,
                    ValueString = "My brother plays football well.",
                    UITypeId = UIType.uiText
                };
                db.Add(childTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lTranslate,
                    ValueString = "My brother plays good football.",
                    UITypeId = UIType.uiText
                };
                db.Add(childTaskItem);

                db.SaveChanges();
            }
        }

        private void AddTask_2()
        {
            using (var db = new ClientDBContext())
            {
                Task task = new Task
                {
                    Text = "Вчера в 9 вечера я выполнял домашнее задание."
                };
                db.Add(task);

                TaskItem parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lTense,
                    SeqNo = 1,
                    UITypeId = UIType.uiSelect
                };
                db.Add(parentTaskItem);
                TaskItem childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lTense,
                    ValueInt = VerbTense.vtPast,
                    UITypeId = UIType.uiSelect
                };
                db.Add(childTaskItem);

                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lAspect,
                    SeqNo = 2,
                    UITypeId = UIType.uiSelect
                };
                db.Add(parentTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lAspect,
                    ValueInt = VerbAspect.vaContinuous,
                    UITypeId = UIType.uiSelect
                };
                db.Add(childTaskItem);


                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lSentencePart,
                    SeqNo = 3,
                    UITypeId = UIType.uiFormula
                };
                db.Add(parentTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childTaskItem);

                TaskItem childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 1,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);

                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spSubject,
                    SeqNo = 2,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);

                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = ModalVerb.mvWas,
                    SeqNo = 3,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);

                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = NotionalVerb.nvVing,
                    SeqNo = 4,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);

                childChildTaskItem = new TaskItem
                {
                    ParentId = childTaskItem.TaskItemId,
                    LangItemId = Lib.lSentencePart,
                    ValueInt = SentencePart.spOtherPart,
                    SeqNo = 5,
                    UITypeId = UIType.uiFormula
                };
                db.Add(childChildTaskItem);


                parentTaskItem = new TaskItem
                {
                    TaskId = task.TaskId,
                    LangItemId = Lib.lTranslate,
                    SeqNo = 4,
                    UITypeId = UIType.uiText
                };
                db.Add(parentTaskItem);
                childTaskItem = new TaskItem
                {
                    ParentId = parentTaskItem.TaskItemId,
                    LangItemId = Lib.lTranslate,
                    ValueString = "Yesterday at 9 pm I was doing my homework.",
                    UITypeId = UIType.uiText
                };
                db.Add(childTaskItem);


                db.SaveChanges();
            }
        }
    }
}