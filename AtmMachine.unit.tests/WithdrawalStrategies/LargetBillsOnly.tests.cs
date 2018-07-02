using System;
using Xunit;
using Moq;
using ATMMachine.Interfaces;
using ATMMachine.Entities;
using ATMMachine.WithdrawalStrategies;

namespace AtmMachine.unit.tests.WithdrawalStrategies
{
    public class LargetBillsOnlyTests
    {
        [Fact]
        public void GivenAdequatelyStockedInventoryWhenWithdrawalIsMadeThenSuccessIsIndicated()
        {
            // Arrange
            IWithdrawalStrategy sut = new LargestBillsOnly();
            var amountToWithdraw = 28;
            var atmInventoryMock = new Mock<IAtmInventory>();
            atmInventoryMock.Setup(inv => inv.Withdraw(It.IsAny<IReadOnlyCashTransaction>())).Returns(true);
                            
            // Act
            var actual = sut.Withdraw(amountToWithdraw, atmInventoryMock.Object);

            // Assert
            Assert.True(actual.IsSuccess, "Expecting a success response but instead got failure.");
            Assert.True(string.IsNullOrEmpty(actual.FailureReason), $"Expected empty or null instead {actual.FailureReason}");
            Assert.Equal<int>(amountToWithdraw, actual.Details.TotalAmount);
            Assert.Equal<int>(1, actual.Details.BillCount(UnitedStatesTender.TwentyDollar));
            Assert.Equal<int>(1, actual.Details.BillCount(UnitedStatesTender.FiveDollar));
            Assert.Equal<int>(3, actual.Details.BillCount(UnitedStatesTender.OneDollar));
        }

        [Fact]
        public void GivenInAdequatelyStockedInventoryWhenWithdrawalIsMadeThenFailureIsIndicated()
        {
            // Arrange
            var expectedFailureReason = "insufficient funds";
            IWithdrawalStrategy sut = new LargestBillsOnly();
            var amountToWithdraw = 28;
            var atmInventoryMock = new Mock<IAtmInventory>();
            atmInventoryMock.Setup(inv => inv.Withdraw(It.IsAny<IReadOnlyCashTransaction>())).Returns(false);

            // Act
            var actual = sut.Withdraw(amountToWithdraw, atmInventoryMock.Object);

            // Assert
            Assert.False(actual.IsSuccess, "Expecting a failure response but instead got success.");
            Assert.True((string.Compare(expectedFailureReason, actual.FailureReason, true) == 0), $"Expected '{expectedFailureReason}' actual '{actual.FailureReason}'");
            Assert.Equal<int>(amountToWithdraw, actual.Details.TotalAmount);
            Assert.Equal<int>(1, actual.Details.BillCount(UnitedStatesTender.TwentyDollar));
            Assert.Equal<int>(1, actual.Details.BillCount(UnitedStatesTender.FiveDollar));
            Assert.Equal<int>(3, actual.Details.BillCount(UnitedStatesTender.OneDollar));
        }
    }
}
