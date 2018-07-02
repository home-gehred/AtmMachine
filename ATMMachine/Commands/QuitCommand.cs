using System;
using ATMMachine.interfaces;

namespace ATMMachine.Commands
{
    public class QuitCommand : AtmCommandBase
    {
        public override bool IsExit => true;

        public override void Execute(IAtmMachine atm)
        {
            throw new ApplicationException("Execute on quit command should not happen.");
        }
    }
}
