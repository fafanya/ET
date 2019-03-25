using System;
using System.Collections.Generic;
using System.Text;

namespace Textbook
{
    public class FormulaItem
    {
        public static readonly FormulaItem ModalVerb = new FormulaItem("Модальный глагол");
        public static readonly FormulaItem Subject = new FormulaItem("Подлежащее");
        public static readonly FormulaItem NotionalVerb = new FormulaItem("Смысловой глагол");
        public static readonly FormulaItem OtherPart = new FormulaItem("Другие части предложения");

        public string FormulaItemTypeID { get; private set; }

        protected FormulaItem(string formulaItemTypeID)
        {
            FormulaItemTypeID = formulaItemTypeID;
        }
    }

    public class ModalVerbFormulaItem : FormulaItem
    {
        public static readonly ModalVerbFormulaItem Do = new ModalVerbFormulaItem("Do");
        public static readonly ModalVerbFormulaItem Was = new ModalVerbFormulaItem("Was");
        public static readonly ModalVerbFormulaItem Were = new ModalVerbFormulaItem("Were");
        public static readonly ModalVerbFormulaItem Been = new ModalVerbFormulaItem("Been");

        public string ModalVerbFormulaItemID { get; private set; }

        private ModalVerbFormulaItem(string modalVerbFormulaItemID) : base(ModalVerb.FormulaItemTypeID)
        {
            ModalVerbFormulaItemID = modalVerbFormulaItemID;
        }
    }

    public class NotionalVerbFormulaItem : FormulaItem
    {
        public static readonly NotionalVerbFormulaItem V = new NotionalVerbFormulaItem("V");
        public static readonly NotionalVerbFormulaItem Ving = new NotionalVerbFormulaItem("Ving");
        public static readonly NotionalVerbFormulaItem Vs = new NotionalVerbFormulaItem("Vs");
        public static readonly NotionalVerbFormulaItem Ves = new NotionalVerbFormulaItem("Ves");

        public string NotionalVerbFormulaItemID { get; private set; }

        private NotionalVerbFormulaItem(string notionalVerbFormulaItemID) : base(NotionalVerb.FormulaItemTypeID)
        {
            NotionalVerbFormulaItemID = notionalVerbFormulaItemID;
        }
    }
}