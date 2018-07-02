using System;
using System.Collections.Generic;
using ATMMachine.interfaces;

namespace ATMMachine.Entities
{
    public class CashTransaction : IReadOnlyCashTransaction
    {
        private IDictionary<UnitedStatesTender, int> _details;

        private CashTransaction()
        {
            _details = new Dictionary<UnitedStatesTender, int>();
        }

        public static CashTransaction Start()
        {
            return new CashTransaction();
        }

        public CashTransaction Add(UnitedStatesTender tender, int numberOfBills)
        {
            if (_details.ContainsKey(tender))
            {
                _details[tender] += numberOfBills;
            }
            else
            {
                _details.Add(new KeyValuePair<UnitedStatesTender, int>(tender, numberOfBills));
            }
            return this;
        }

        public int TotalAmount
        {
            get
            {
                var amount = 0;
                foreach(var detail in _details)
                {
                    amount += detail.Key.GetValue(detail.Value);
                }
                return amount;
            }
        }

        public int BillCount(UnitedStatesTender tender)
        {
            if (_details.ContainsKey(tender))
            {
                return _details[tender];
            }

            return 0;
        }

    }
}
