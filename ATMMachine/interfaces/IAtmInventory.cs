using System;
using System.Collections.Generic;
using ATMMachine.Entities;
using ATMMachine.Interfaces;

namespace ATMMachine.Interfaces
{
    public interface IAtmInventory
    {
        void ResetInventory(IReadOnlyCashTransaction restock);
        bool Withdraw(IReadOnlyCashTransaction cashTransaction);
        int GetBillCount(UnitedStatesTender tender);
        IReadOnlyCashTransaction MachineBalance();
        IReadOnlyCashTransaction InventoryByBills(IReadOnlyList<UnitedStatesTender> bills);
    }
}
