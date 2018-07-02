﻿using System;
using ATMMachine.interfaces;

namespace ATMMachine.Commands
{
    public class InvalidCommand : AtmCommandBase
    {
        public InvalidCommand()
        {
        }

        public override bool IsExit => false;

        public override void Execute(IAtmMachine atm)
        {
            Console.WriteLine("Failure: Invalid Command");
        }
    }
}
