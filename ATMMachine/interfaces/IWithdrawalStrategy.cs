using System;
namespace ATMMachine.Interfaces
{
    
    public interface IWithdrawalStrategy
    {
        IWithdrawalResult Withdraw(int amount, IAtmInventory inventory);
    }
}
