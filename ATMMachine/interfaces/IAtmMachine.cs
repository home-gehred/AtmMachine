using System.Collections.Generic;
using ATMMachine.Entities;

namespace ATMMachine.Interfaces
{
    public interface IAtmMachine
    {
        IReadOnlyCashTransaction InventoryByBills(IReadOnlyList<UnitedStatesTender> bills);
        IReadOnlyCashTransaction MachineBalance();
        void Restock(IReadOnlyCashTransaction restock);
        IWithdrawalResult Withdrawal(int amount);
    }
}