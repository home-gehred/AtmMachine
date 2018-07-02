using System;
using ATMMachine.interfaces;

namespace ATMMachine.Commands
{
    public class WithdrawCommand : AtmCommandBase
    {
        private int _amount;

        public WithdrawCommand(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Value must be greater then zero.");
            }
            _amount = amount;
        }

        public WithdrawCommand(string input)
        {
            var commandKey = "W $";
            if (input.StartsWith("W $", StringComparison.InvariantCulture))
            {
                var amountInput = input.Substring(commandKey.Length);
                if (int.TryParse(amountInput, out _amount) == false)
                {
                    throw new ApplicationException($"Please call IsWithdrawInputValid before using this constructor. {nameof(input)} - {input}");
                }
            }
            else
            {
                throw new ApplicationException($"Please call IsWithdrawInputValid before using this constructor. {nameof(input)} - {input}");
            }
        }

        public static bool IsWithdrawInputValid(string userInput)
        {
            var commandKey = "W $";
            if (userInput.StartsWith(commandKey, StringComparison.InvariantCulture))
            {
                var amount = userInput.Substring(commandKey.Length);
                return int.TryParse(amount, out int result);
            }
            return false;
        }

        public override bool IsExit => false;

        public override void Execute(IAtmMachine atm)
        {
            var result = atm.Withdrawal(_amount);

            if (result.IsSuccess)
            {
                Console.WriteLine($"Success: Dispensed ${result.Details.TotalAmount}");
                var balance = atm.MachineBalance();
                DisplayBalance(balance);
            }
            else
            {
                Console.WriteLine($"Failure: {result.FailureReason}");
                // The specification is unclear, in the word document it says every withdrawal should
                // display the atm balance. However the example does not display the balance on a failure.
                // TODO: Talk with stake holder to update requirements or example to be correct.
            }
        }
    }
}
