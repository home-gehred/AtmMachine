using System;
using System.Collections.Generic;
using Xunit;
using ATMMachine.Entities;

namespace AtmMachine.unit.tests.Entities
{
    public class AtmInventoryTests
    {
        [Fact]
        public void GivenEmptyInventoryWhenRestockedThenBalanceIsExpected()
        {
            // Arrange
            var sut = new AtmInventory();
            var expectedInventory = CashTransaction.Start();
            expectedInventory.Add(UnitedStatesTender.FiveDollar, 10);
            expectedInventory.Add(UnitedStatesTender.OneDollar, 10);

            // Act
            sut.ResetInventory(expectedInventory);

            // Assert
            var actual = sut.MachineBalance();
            Assert.Equal<int>(60, actual.TotalAmount);
            Assert.Equal<int>(10, actual.BillCount(UnitedStatesTender.FiveDollar));
            Assert.Equal<int>(10, actual.BillCount(UnitedStatesTender.OneDollar));
        }

        [Fact]
        public void GivenStockedInventoryWhenRequestingInventoryByBillsThenOnlyBillsThatAreReuestedAreReturned()
        {
            // Arrange
            var sut = new AtmInventory();
            var expectedInventory = CashTransaction.Start();
            expectedInventory.Add(UnitedStatesTender.TenDollar, 1000);
            expectedInventory.Add(UnitedStatesTender.FiveDollar, 100);
            expectedInventory.Add(UnitedStatesTender.OneDollar, 10);
            sut.ResetInventory(expectedInventory);
            var billsToGet = new List<UnitedStatesTender>();
            billsToGet.Add(UnitedStatesTender.OneDollar);
            billsToGet.Add(UnitedStatesTender.TenDollar);

            // Act
            var actual = sut.InventoryByBills(billsToGet);

            // Assert
            Assert.Equal<int>(1000, actual.BillCount(UnitedStatesTender.TenDollar));
            Assert.Equal<int>(0, actual.BillCount(UnitedStatesTender.FiveDollar));
            Assert.Equal<int>(10, actual.BillCount(UnitedStatesTender.OneDollar));
        }
    }
}
