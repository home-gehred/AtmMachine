using System;
using ATMMachine.Entities;
using ATMMachine.WithdrawalStrategies;
using ATMMachine.Commands;

namespace ATMMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var withdrawalStrategy = new LargestBillsOnly();
            var inventory = new AtmInventory();
            inventory.ResetInventory(AtmInventory.DefaultInventory());
            var atm = new AtmMachine(withdrawalStrategy, inventory);
            var commandFactory = new CommandFactory();
            var done = false;
            do
            {
                var input = Console.ReadLine();
                var command = commandFactory.CreateCommand(input);

                if (command.IsExit)
                {
                    done = true;
                }
                else
                {
                    command.Execute(atm);
                }

            } while (done != true);
        }
    }
}
