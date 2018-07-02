using System;
using System.Collections.Generic;
using ATMMachine.interfaces;

namespace ATMMachine.Entities
{
    public class AtmInventory : IAtmInventory
    {
        private IDictionary<UnitedStatesTender, int> _billInventory;

        public AtmInventory()
        {
            _billInventory = new Dictionary<UnitedStatesTender, int>();
            var allTenders = UnitedStatesTender.GetAllDefinedTenders();
            foreach(var tender in allTenders)
            {
                _billInventory.Add(tender, 0);    
            }
        }

        public static IReadOnlyCashTransaction DefaultInventory()
        {
            var inventory = CashTransaction.Start();

            inventory.Add(UnitedStatesTender.HundredDollar, 10);
            inventory.Add(UnitedStatesTender.FiftyDollar, 10);
            inventory.Add(UnitedStatesTender.TwentyDollar, 10);
            inventory.Add(UnitedStatesTender.TenDollar, 10);
            inventory.Add(UnitedStatesTender.FiveDollar, 10);
            inventory.Add(UnitedStatesTender.OneDollar, 10);

            return inventory;
        }

        public bool Withdraw(IReadOnlyCashTransaction cashTransaction)
        {
            bool isValid = true;
            foreach (var key in _billInventory.Keys)
            {
                isValid = isValid && (_billInventory[key] >= cashTransaction.BillCount(key));
                if (isValid == false)
                {
                    break;
                }
            }
            // Only set the inventory if the withdraw can happen.
            if (isValid == true)
            {
                foreach (var bill in UnitedStatesTender.GetAllDefinedTenders())
                {
                    if (_billInventory.ContainsKey(bill))
                    {
                        _billInventory[bill] = _billInventory[bill] - cashTransaction.BillCount(bill);
                    }
                }
            }
            return isValid;
        }

        public void ResetInventory(IReadOnlyCashTransaction restock)
        {
            foreach (var key in UnitedStatesTender.GetAllDefinedTenders())
            {
                if (_billInventory.ContainsKey(key))
                {
                    _billInventory[key] = restock.BillCount(key);
                }
            }
        }

        public int GetBillCount(UnitedStatesTender tender)
        {
            if (_billInventory.ContainsKey(tender))
            {
                return _billInventory[tender];
            }
            return 0;
        }

        public IReadOnlyCashTransaction MachineBalance()
        {
            var balance = CashTransaction.Start();
            foreach(var key in _billInventory.Keys)
            {
                balance.Add(key, _billInventory[key]);
            }
            return balance;
        }

        public IReadOnlyCashTransaction InventoryByBills(IReadOnlyList<UnitedStatesTender> bills)
        {
            var requestedBillCount = CashTransaction.Start();
            foreach(var requestedBill in bills)
            {
                if (_billInventory.ContainsKey(requestedBill))
                {
                    requestedBillCount.Add(requestedBill, _billInventory[requestedBill]);
                }
                else
                {
                    requestedBillCount.Add(requestedBill, 0);
                }
            }
            return requestedBillCount;
        }
    }
}
