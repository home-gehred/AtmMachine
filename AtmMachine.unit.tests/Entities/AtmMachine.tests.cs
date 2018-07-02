using System;
using Xunit;
using ATMMachine.Interfaces;
using ATMMachine.Entities;

namespace AtmMachine.unit.tests.Entities
{
    public class AtmMachineTests
    {
        [Fact]
        public void ConstructWithNullWithdrawalStrategyShouldThrow()
        {
            // Arrange
            IWithdrawalStrategy testWithdrawalStrategy = null;
            var testInventory = new Moq.Mock<IAtmInventory>();

            // Act
            var actualException = Assert.Throws<ArgumentNullException>(() => {
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
                new ATMMachine.Entities.AtmMachine(testWithdrawalStrategy, testInventory.Object);
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
            });

            // Assert
            Assert.Contains("Value cannot be null", actualException.Message);
        }

        [Fact]
        public void ConstructWithNullInventoryShouldThrow()
        {
            // Arrange
            var testWithdrawalStrategy = new Moq.Mock<IWithdrawalStrategy>();
            IAtmInventory testInventory = null;

            // Act
            var actualException = Assert.Throws<ArgumentNullException>(() => {
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
                new ATMMachine.Entities.AtmMachine(testWithdrawalStrategy.Object, testInventory);
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
            });

            // Assert
            Assert.Contains("Value cannot be null", actualException.Message);
        }
    }
}
