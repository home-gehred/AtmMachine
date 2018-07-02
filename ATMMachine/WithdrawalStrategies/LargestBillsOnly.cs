using System;
using System.Linq;
using ATMMachine.Interfaces;
using ATMMachine.Entities;

namespace ATMMachine.WithdrawalStrategies
{
    public class LargestBillsOnly : IWithdrawalStrategy
    {
        public IWithdrawalResult Withdraw(int amount, IAtmInventory inventory)
        {
            var withdrawTransaction = CashTransaction.Start();
            var descendingUnitedStatesTenders = UnitedStatesTender.GetAllDefinedTenders().OrderByDescending(tender => tender.Value);

            var transactionAmount = amount;
            foreach(var tender in descendingUnitedStatesTenders)
            {
                var numberOfBills = transactionAmount / tender.Value;
                if (numberOfBills > 0)
                {
                    withdrawTransaction.Add(tender, numberOfBills);
                    transactionAmount = transactionAmount - tender.GetValue(numberOfBills);
                }
                if (transactionAmount == 0) 
                {
                    break;
                }
                if (transactionAmount < 0)
                {
                    throw new ApplicationException($"transaction amount has gone below zero!");
                }                    
            }

            // Does inventory support transaction
            var isPossible = inventory.Withdraw(withdrawTransaction);
            if (isPossible)
            {
                return WithdrawalResult.CreateSuccessResult(withdrawTransaction);
            }
            else
            {
                return WithdrawalResult.CreateFailureResult("insufficient funds", withdrawTransaction);
            }
        }
    }
}
