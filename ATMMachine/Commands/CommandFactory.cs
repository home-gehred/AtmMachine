using System;
namespace ATMMachine.Commands
{
    public class CommandFactory
    {
        public AtmCommandBase CreateCommand(string input)
        {
            var invalidCommand = new InvalidCommand();
            if (string.Compare("Q", input, false) == 0)
            {
                return new QuitCommand();
            }
            if (input.StartsWith('W'))
            {
                if (WithdrawCommand.IsWithdrawInputValid(input))
                {
                    return new WithdrawCommand(input);
                }
            }
            if (input.StartsWith('I'))
            {
                if (InventoryCommand.IsInventoryInputValid(input))
                {
                    return new InventoryCommand(input);
                }
            }
            if (string.Compare("R", input, false) == 0)
            {
                return new RestockCommand();
            }

            return invalidCommand;
        }
    }
}
