using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace ATMMachine.Entities
{
    [DebuggerDisplay("Name: {_name}")]  
    public class UnitedStatesTender
    {
        public static UnitedStatesTender OneDollar = new UnitedStatesTender(1, "one dollar");
        public static UnitedStatesTender FiveDollar = new UnitedStatesTender(5, "five dollar");
        public static UnitedStatesTender TenDollar = new UnitedStatesTender(10, "ten dollar");
        public static UnitedStatesTender TwentyDollar = new UnitedStatesTender(20, "twenty dollar");
        public static UnitedStatesTender FiftyDollar = new UnitedStatesTender(50, "fifty dollar");
        public static UnitedStatesTender HundredDollar = new UnitedStatesTender(100, "hundred dollar");

        private int _value;
        private string _name;

        private UnitedStatesTender(int value, string name)
        {
            _value = value;
            _name = name.ToLower();
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int GetValue(int numberOfBills)
        {
            return _value * numberOfBills;
        }

        public static IReadOnlyList<UnitedStatesTender> GetAllDefinedTenders()
        {
            var tenders = new List<UnitedStatesTender>();
            tenders.Add(OneDollar);
            tenders.Add(FiveDollar);
            tenders.Add(TenDollar);
            tenders.Add(TwentyDollar);
            tenders.Add(FiftyDollar);
            tenders.Add(HundredDollar);
            return tenders;
        }

        public static bool TryParse(string amount, out UnitedStatesTender bill)
        {
            var cleanAmount = amount;
            if (cleanAmount.StartsWith('$'))
            {
                cleanAmount = cleanAmount.Substring(1);
            }
            if (int.TryParse(cleanAmount, out int tenderValue))
            {
                var allTenders = GetAllDefinedTenders();
                foreach(var tender in allTenders)
                {
                    if (tender.Value == tenderValue)
                    {
                        bill = tender;
                        return true;
                    }
                }
            }
            bill = null;
            return false;
        }
    }
}
