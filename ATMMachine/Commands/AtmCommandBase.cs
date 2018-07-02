using System;
using System.Linq;
using ATMMachine.interfaces;
using ATMMachine.Entities;

namespace ATMMachine.Commands
{
    public abstract class AtmCommandBase
    {
        public abstract bool IsExit { get; }
        public abstract void Execute(IAtmMachine atm);

        protected void DisplayBalance(IReadOnlyCashTransaction balance)
        {
            Console.WriteLine("Machine Balance:");
            var ordered = UnitedStatesTender.GetAllDefinedTenders().OrderByDescending(tender => tender.Value);
            foreach(var bill in ordered)
            {
                Console.WriteLine($"${bill.Value} - {balance.BillCount(bill)}");
            }
        }
    }
}
