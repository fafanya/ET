using System;
using System.Collections.Generic;
using System.Text;
using Textbook;

namespace Workbook
{
    public static class TestCreator
    {
        public static Test Create()
        {
            Test test = new Test();

            Task task = new Task
            {
                NativeLangText = "Мой брат хорошо играет в футбол.",
                TranslationsList = new string[]
                {
                    "My brother plays football well",
                    "My brother plays good football"
                },
                VerbTense = VerbTense.Present,
                VerbAspect = VerbAspect.Simple,
                TaskType = TaskType.MakeTense,
                CompositionList = new List<string[]>
                {
                    new string[]
                    {
                        SentencePart.spAttribute,
                        SentencePart.spSubject,
                        SentencePart.spPredicate,
                        SentencePart.spObject,
                        SentencePart.spAdverbialModifier
                    },
                    new string[]
                    {
                        SentencePart.spAttribute,
                        SentencePart.spSubject,
                        SentencePart.spPredicate,
                        SentencePart.spAttribute,
                        SentencePart.spObject
                    }
                }
            };
            test.AddTask(task);

            task = new Task
            {
                NativeLangText = "Вчера в 9 вечера я выполнял домашнее задание.",
                TranslationsList = new string[]
                {
                    "Yesterday at 9 pm I was doing my homework.",
                },
                VerbTense = VerbTense.Past,
                VerbAspect = VerbAspect.Continuous,
                TaskType = TaskType.MakeTense,
                CompositionList = new List<string[]>
                {
                    new string[]
                    {
                        SentencePart.spAdverbialModifier,
                        SentencePart.spSubject,
                        SentencePart.spPredicate,
                        SentencePart.spObject
                    }
                }
            };
            test.AddTask(task);

            return test;
        }
    }
}