using System;
using System.Collections.Generic;
using ATMMachine.Entities;
using ATMMachine.interfaces;

namespace ATMMachine.Commands
{
    public class InventoryCommand : AtmCommandBase
    {
        private List<UnitedStatesTender> _tenders;

        public InventoryCommand(string input)
        {
            _tenders = new List<UnitedStatesTender>();
            var commandKey = "I $";
            if (input.StartsWith(commandKey, StringComparison.InvariantCulture))
            {
                var userBills = input.Substring((commandKey.Length - 1));
                var billAmounts = userBills.Split('$', StringSplitOptions.RemoveEmptyEntries);
                var isValid = true;
                foreach (var billAmount in billAmounts)
                {
                    UnitedStatesTender tender = null;
                    isValid = isValid && UnitedStatesTender.TryParse(billAmount, out tender);
                    if (tender != null)
                    {
                        _tenders.Add(tender);
                    }
                }

                if (isValid == false)
                {
                    throw new ApplicationException($"Please call IsInventoryInputValid before using this constructor. {nameof(input)} - {input}");
                }
            }
            else
            {
                throw new ApplicationException($"Please call IsInventoryInputValid before using this constructor. {nameof(input)} - {input}");
            }

        }

        public static bool IsInventoryInputValid(string userInput)
        {
            var commandKey = "I $";
            if (userInput.StartsWith(commandKey, StringComparison.InvariantCulture))
            {
                var userBills = userInput.Substring((commandKey.Length - 1));
                var billAmounts = userBills.Split('$', StringSplitOptions.RemoveEmptyEntries);
                var isValid = true;
                foreach(var billAmount in billAmounts)
                {
                    isValid = isValid && UnitedStatesTender.TryParse(billAmount, out UnitedStatesTender tender);
                }

                return isValid;
            }
            return false;
        }

        public override bool IsExit => false;

        public override void Execute(IAtmMachine atm)
        {
            var inventoryOfSelected = atm.InventoryByBills(_tenders);
            foreach(var tender in _tenders)
            {
                Console.WriteLine($"${tender.Value} - {inventoryOfSelected.BillCount(tender)}");
            }
        }
    }
}
