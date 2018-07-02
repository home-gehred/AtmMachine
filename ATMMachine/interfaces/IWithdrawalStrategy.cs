using System;
namespace ATMMachine.interfaces
{
    
    public interface IWithdrawalStrategy
    {
        IWithdrawalResult Withdraw(int amount, IAtmInventory inventory);
    }
}
