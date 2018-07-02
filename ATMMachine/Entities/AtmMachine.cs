using System;
using System.Collections.Generic;
using ATMMachine.Interfaces;

namespace ATMMachine.Entities
{
    public class AtmMachine : IAtmMachine
    {
        private IWithdrawalStrategy _withdrawalStrategy;
        private IAtmInventory _atmInventory;

        public AtmMachine(IWithdrawalStrategy withdrawalStrategy, IAtmInventory atmInventory)
        {
            if (withdrawalStrategy == null)
            {
                throw new ArgumentNullException(nameof(withdrawalStrategy));
            }
            if (atmInventory == null)
            {
                throw new ArgumentNullException(nameof(atmInventory));
            }

            _withdrawalStrategy = withdrawalStrategy;
            _atmInventory = atmInventory;
        }

        public void Restock(IReadOnlyCashTransaction restock)
        {
            _atmInventory.ResetInventory(restock);
        }

        public IWithdrawalResult Withdrawal(int amount)
        {
            return _withdrawalStrategy.Withdraw(amount, _atmInventory);
        }

        public IReadOnlyCashTransaction MachineBalance()
        {
            return _atmInventory.MachineBalance();
        }

        public IReadOnlyCashTransaction InventoryByBills(IReadOnlyList<UnitedStatesTender> bills)
        {
            return _atmInventory.InventoryByBills(bills);
        }
    }
}
