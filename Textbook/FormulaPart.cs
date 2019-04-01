using System;
using System.Collections.Generic;
using System.Text;

namespace Textbook
{
    [Serializable]
    public class FormulaItem
    {
        public static readonly FormulaItem ModalVerb = new FormulaItem("Модальный глагол", 11);
        public static readonly FormulaItem Subject = new FormulaItem("Подлежащее", 12);
        public static readonly FormulaItem NotionalVerb = new FormulaItem("Смысловой глагол", 13);
        public static readonly FormulaItem OtherPart = new FormulaItem("Другие части предложения", 14);

        public string FormulaItemTypeUID { get; private set; }
        public int FormulaItemTypeID { get; private set; }

        protected FormulaItem(string formulaItemTypeUID, int formulaItemTypeID)
        {
            FormulaItemTypeUID = formulaItemTypeUID;
        }
    }

    [Serializable]
    public class ModalVerbFormulaItem : FormulaItem
    {
        public static readonly ModalVerbFormulaItem Do = new ModalVerbFormulaItem("Do", 21);
        public static readonly ModalVerbFormulaItem Was = new ModalVerbFormulaItem("Was", 22);
        public static readonly ModalVerbFormulaItem Were = new ModalVerbFormulaItem("Were", 23);
        public static readonly ModalVerbFormulaItem Been = new ModalVerbFormulaItem("Been", 24);

        public string ModalVerbFormulaItemUID { get; private set; }
        public int ModalVerbFormulaItemID { get; private set; }

        private ModalVerbFormulaItem(string modalVerbFormulaItemUID, int modalVerbFormulaItemID) : base(ModalVerb.FormulaItemTypeUID, ModalVerb.FormulaItemTypeID)
        {
            ModalVerbFormulaItemUID = modalVerbFormulaItemUID;
            ModalVerbFormulaItemID = modalVerbFormulaItemID;
        }
    }

    [Serializable]
    public class NotionalVerbFormulaItem : FormulaItem
    {
        public static readonly NotionalVerbFormulaItem V = new NotionalVerbFormulaItem("V", 31);
        public static readonly NotionalVerbFormulaItem Ving = new NotionalVerbFormulaItem("Ving", 32);
        public static readonly NotionalVerbFormulaItem Vs = new NotionalVerbFormulaItem("Vs", 33);
        public static readonly NotionalVerbFormulaItem Ves = new NotionalVerbFormulaItem("Ves", 34);

        public string NotionalVerbFormulaItemUID { get; private set; }
        public int NotionalVerbFormulaItemID { get; private set; }

        private NotionalVerbFormulaItem(string notionalVerbFormulaItemUID, int notionalVerbFormulaItemID) : base(NotionalVerb.FormulaItemTypeUID, ModalVerb.FormulaItemTypeID)
        {
            NotionalVerbFormulaItemUID = notionalVerbFormulaItemUID;
            NotionalVerbFormulaItemID = notionalVerbFormulaItemID;
        }
    }
}