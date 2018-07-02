using ATMMachine.Entities;
using System;
using Xunit;


namespace AtmMachine.unit.tests.Entities
{
    public class UnitedStatesTenderTests
    {
        [Fact]
        public void GivenOneDollarWhenGettingAttributesThenValueAndNameAreCorrect()
        {
            // Arrange
            var expectedName = "one dollar";
            // Act
            // Assert
            Assert.Equal<int>(1, UnitedStatesTender.OneDollar.Value);
            Assert.True(string.Compare(expectedName, UnitedStatesTender.OneDollar.Name, false) == 0, $"Expecting '{expectedName}' found {UnitedStatesTender.OneDollar.Name}");
        }

        [Fact]
        public void GivenFiveDollarWhenGettingAttributesThenValueAndNameAreCorrect()
        {
            // Arrange
            var expectedName = "five dollar";
            // Act
            // Assert
            Assert.Equal<int>(5, UnitedStatesTender.FiveDollar.Value);
            Assert.True(string.Compare(expectedName, UnitedStatesTender.FiveDollar.Name, false) == 0, $"Expecting '{expectedName}' found {UnitedStatesTender.OneDollar.Name}");
        }

        [Fact]
        public void GivenFiveDollarWhenSettingTheNumberOfBillsThenAmountIsCorrect()
        {
            // Arrange
            var expectedAmount = 55;

            // Act
            var actual = UnitedStatesTender.FiveDollar.GetValue(11);

            // Assert
            Assert.Equal<int>(expectedAmount, actual);
        }

        [Fact]
        public void GivenAValidDollarAmountWhenTryParseThenGetExpectedTender()
        {
            // Arrange
            var testAmount = "50";

            // Act
            var actualResult = UnitedStatesTender.TryParse(testAmount, out UnitedStatesTender actualTender);

            // Assert
            Assert.True(actualResult, "Expected try parse to work");
            Assert.Equal<int>(UnitedStatesTender.FiftyDollar.Value, actualTender.Value);
        }

        [Fact]
        public void GivenAnInvalidBillAmountWhenTryParseThenGetExpectedTender()
        {
            // Arrange
            var testAmount = "3";

            // Act
            var actualResult = UnitedStatesTender.TryParse(testAmount, out UnitedStatesTender actualTender);

            // Assert
            Assert.False(actualResult, "Expected try parse to not work");
            Assert.True(actualTender == null, "Expected null.");
        }

    }
}

