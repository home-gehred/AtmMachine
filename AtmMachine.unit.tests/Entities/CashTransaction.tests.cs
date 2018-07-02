using System;
using ATMMachine.Entities;
using Xunit;

namespace AtmMachine.unit.tests.Entities
{
    public class CashTransactionTests
    {
        [Fact]
        public void ConstructATransaction()
        {
            // arrange
            var expectedFiveDollarBills = 3;
            var expectedOneDollarBills = 2;
            var expectedAmount = 17;
            CashTransaction testTransaction = CashTransaction.Start();
            // act
            testTransaction
                .Add(UnitedStatesTender.FiveDollar, expectedFiveDollarBills)
                .Add(UnitedStatesTender.OneDollar, expectedOneDollarBills);

            // assert
            Assert.Equal<int>(expectedAmount, testTransaction.TotalAmount);
            Assert.Equal<int>(expectedFiveDollarBills, testTransaction.BillCount(UnitedStatesTender.FiveDollar));
            Assert.Equal<int>(expectedOneDollarBills, testTransaction.BillCount(UnitedStatesTender.OneDollar));
        }
    }
}
