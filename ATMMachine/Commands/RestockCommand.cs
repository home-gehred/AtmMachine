using System;
using ATMMachine.Entities;
using ATMMachine.interfaces;

namespace ATMMachine.Commands
{
    public class RestockCommand : AtmCommandBase
    {
        public override bool IsExit => false;

        public override void Execute(IAtmMachine atm)
        {
            atm.Restock(AtmInventory.DefaultInventory());
            DisplayBalance(atm.MachineBalance());
        }
    }
}
